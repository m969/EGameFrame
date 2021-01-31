using System;
using System.IO;
using EGameFrame;
using EGameFrame.Message;

namespace ET
{
    public class ActorLocationSenderComponent: Entity
    {
        //[NoMemoryCheck]
        public static long TIMEOUT_TIME = 10 * 1000;
        
        public static ActorLocationSenderComponent Instance { get; set; }

        public long CheckTimer;


        public void Check()
        {
            using (ListComponent<long> list = ListComponent<long>.Create())
            {
                long timeNow = TimeHelper.ServerNow();
                foreach (Entity value in this.Children)
                {
                    ActorLocationSender actorLocationMessageSender = (ActorLocationSender)value;

                    if (timeNow > actorLocationMessageSender.LastSendOrRecvTime + ActorLocationSenderComponent.TIMEOUT_TIME)
                    {
                        list.List.Add(value.Id);
                    }
                }

                foreach (long id in list.List)
                {
                    this.Remove(id);
                }
            }
        }

        private ActorLocationSender GetOrCreate(long id)
        {
            if (id == 0)
            {
                throw new Exception($"actor id is 0");
            }

            if (this.IdChildren.TryGetValue(id, out Entity actorLocationSender))
            {
                return (ActorLocationSender)actorLocationSender;
            }

            actorLocationSender = EntityFactory.CreateWithParent<ActorLocationSender>(this, id);
            return (ActorLocationSender)actorLocationSender;
        }

        private void Remove(long id)
        {
            if (!this.IdChildren.TryGetValue(id, out Entity actorMessageSender))
            {
                return;
            }

            actorMessageSender.Dispose();
        }

        public void Send(long entityId, IActorRequest message)
        {
            this.Call(entityId, message).Coroutine();
        }

        public async ETTask<IActorResponse> Call(long entityId, IActorRequest iActorRequest)
        {
            ActorLocationSender actorLocationSender = this.GetOrCreate(entityId);

            // 先序列化好
            int rpcId = ActorMessageSenderComponent.Instance.GetRpcId();
            iActorRequest.RpcId = rpcId;
            (ushort _, MemoryStream stream) = MessageSerializeHelper.MessageToStream(0, iActorRequest);

            long actorLocationSenderInstanceId = actorLocationSender.InstanceId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.ActorLocationSender, entityId))
            {
                if (actorLocationSender.InstanceId != actorLocationSenderInstanceId)
                {
                    throw new RpcException(ErrorCode.ERR_ActorTimeout, $"{stream.ToActorMessage()}");
                }

                // 队列中没处理的消息返回跟上个消息一样的报错
                if (actorLocationSender.Error == ErrorCode.ERR_NotFoundActor)
                {
                    return ActorHelper.CreateResponse(iActorRequest, actorLocationSender.Error);
                }

                try
                {
                    return await this.CallInner(actorLocationSender, rpcId, stream);
                }
                catch (RpcException)
                {
                    this.Remove(actorLocationSender.Id);
                    throw;
                }
                catch (Exception e)
                {
                    this.Remove(actorLocationSender.Id);
                    throw new Exception($"{stream.ToActorMessage()}", e);
                }
            }
        }

        private async ETTask<IActorResponse> CallInner(ActorLocationSender actorLocationSender, int rpcId, MemoryStream memoryStream)
        {
            int failTimes = 0;
            long instanceId = actorLocationSender.InstanceId;
            actorLocationSender.LastSendOrRecvTime = TimeHelper.ServerNow();

            while (true)
            {
                if (actorLocationSender.ActorId == 0)
                {
                    actorLocationSender.ActorId = await LocationProxyComponent.Instance.Get(actorLocationSender.Id);
                    if (actorLocationSender.InstanceId != instanceId)
                    {
                        throw new RpcException(ErrorCode.ERR_ActorLocationSenderTimeout2, $"{memoryStream.ToActorMessage()}");
                    }
                }

                if (actorLocationSender.ActorId == 0)
                {
                    IActorRequest iActorRequest = (IActorRequest)memoryStream.ToActorMessage();
                    return ActorHelper.CreateResponse(iActorRequest, ErrorCode.ERR_NotFoundActor);
                }

                IActorResponse response = await ActorMessageSenderComponent.Instance.Call(actorLocationSender.ActorId, rpcId, memoryStream, false);
                if (actorLocationSender.InstanceId != instanceId)
                {
                    throw new RpcException(ErrorCode.ERR_ActorLocationSenderTimeout3, $"{memoryStream.ToActorMessage()}");
                }

                switch (response.Error)
                {
                    case ErrorCode.ERR_NotFoundActor:
                        {
                            // 如果没找到Actor,重试
                            ++failTimes;
                            if (failTimes > 20)
                            {
                                Log.Debug($"actor send message fail, actorid: {actorLocationSender.Id}");
                                actorLocationSender.Error = ErrorCode.ERR_NotFoundActor;
                                // 这里不能删除actor，要让后面等待发送的消息也返回ERR_NotFoundActor，直到超时删除
                                return response;
                            }

                            // 等待0.5s再发送
                            await TimerComponent.Instance.WaitAsync(500);
                            if (actorLocationSender.InstanceId != instanceId)
                            {
                                throw new RpcException(ErrorCode.ERR_ActorLocationSenderTimeout4, $"{memoryStream.ToActorMessage()}");
                            }

                            actorLocationSender.ActorId = 0;
                            continue;
                        }
                    case ErrorCode.ERR_ActorNoMailBoxComponent:
                    case ErrorCode.ERR_ActorTimeout:
                        {
                            throw new RpcException(response.Error, $"{memoryStream.ToActorMessage()}");
                        }
                }

                if (ErrorCode.IsRpcNeedThrowException(response.Error))
                {
                    throw new RpcException(response.Error, $"Message: {response.Message} Request: {memoryStream.ToActorMessage()}");
                }

                return response;
            }
        }
    }
}