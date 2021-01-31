using System.Collections.Generic;
using EGameFrame;
using System;
using System.IO;
using EGameFrame.Message;

namespace ET
{
	public class ActorMessageSenderComponent: Entity
	{
		public static long TIMEOUT_TIME = 30 * 1000;
		
		public static ActorMessageSenderComponent Instance { get; set; }
		
		public int RpcId;
		
		public readonly SortedDictionary<int, ActorMessageSender> requestCallback = new SortedDictionary<int, ActorMessageSender>();

		public long TimeoutCheckTimer;
		
		public List<int> TimeoutActorMessageSenders = new List<int>();

        public static void Run(ActorMessageSender self, IActorResponse response)
        {
            if (response.Error == ErrorCode.ERR_ActorTimeout)
            {
                self.Tcs.SetException(new Exception($"Rpc error: request, 注意Actor消息超时，请注意查看是否死锁或者没有reply: actorId: {self.ActorId} {self.MemoryStream.ToActorMessage()}, response: {response}"));
                return;
            }

            if (self.NeedException && ErrorCode.IsRpcNeedThrowException(response.Error))
            {
                self.Tcs.SetException(new Exception($"Rpc error: actorId: {self.ActorId} request: {self.MemoryStream.ToActorMessage()}, response: {response}"));
                return;
            }

            self.Tcs.SetResult(response);
        }

        public void Check()
        {
            long timeNow = TimeHelper.ServerNow();
            foreach (var item in requestCallback)
            {
                // 因为是顺序发送的，所以，检测到第一个不超时的就退出
                if (timeNow < item.Value.CreateTime + ActorMessageSenderComponent.TIMEOUT_TIME)
                {
                    break;
                }

                this.TimeoutActorMessageSenders.Add(item.Key);
            }

            foreach (int rpcId in this.TimeoutActorMessageSenders)
            {
                ActorMessageSender actorMessageSender = this.requestCallback[rpcId];
                this.requestCallback.Remove(rpcId);
                try
                {
                    IActorResponse response = ActorHelper.CreateResponse((IActorRequest)actorMessageSender.MemoryStream.ToActorMessage(), ErrorCode.ERR_ActorTimeout);
                    Run(actorMessageSender, response);
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
            }

            this.TimeoutActorMessageSenders.Clear();
        }

        public void Send(long actorId, IMessage message)
        {
            if (actorId == 0)
            {
                throw new Exception($"actor id is 0: {message}");
            }

            ProcessActorId processActorId = new ProcessActorId(actorId);
            //Session session = NetInnerComponent.Instance.Get(processActorId.Process);
            //session.Send(processActorId.ActorId, message);
        }

        public void Send(long actorId, MemoryStream memoryStream)
        {
            if (actorId == 0)
            {
                throw new Exception($"actor id is 0: {memoryStream.ToActorMessage()}");
            }

            ProcessActorId processActorId = new ProcessActorId(actorId);
            //Session session = NetInnerComponent.Instance.Get(processActorId.Process);
            //session.Send(processActorId.ActorId, memoryStream);
        }


        public int GetRpcId()
        {
            return ++this.RpcId;
        }

        public async ETTask<IActorResponse> Call(
                long actorId,
                IActorRequest request,
                bool needException = true
        )
        {
            request.RpcId = this.GetRpcId();

            if (actorId == 0)
            {
                throw new Exception($"actor id is 0: {request}");
            }

            (ushort _, MemoryStream stream) = MessageSerializeHelper.MessageToStream(0, request);

            return await this.Call(actorId, request.RpcId, stream, needException);
        }

        public async ETTask<IActorResponse> Call(
                long actorId,
                int rpcId,
                MemoryStream memoryStream,
                bool needException = true
        )
        {
            if (actorId == 0)
            {
                throw new Exception($"actor id is 0: {memoryStream.ToActorMessage()}");
            }

            var tcs = new ETTaskCompletionSource<IActorResponse>();

            this.requestCallback.Add(rpcId, new ActorMessageSender(actorId, memoryStream, tcs, needException));

            this.Send(actorId, memoryStream);

            long beginTime = TimeHelper.ServerFrameTime();
            IActorResponse response = await tcs.Task;
            long endTime = TimeHelper.ServerFrameTime();

            long costTime = endTime - beginTime;
            if (costTime > 200)
            {
                Log.Warning("actor rpc time > 200: {0} {1}", costTime, memoryStream.ToActorMessage());
            }

            return response;
        }

        public void RunMessage(long actorId, IActorResponse response)
        {
            ActorMessageSender actorMessageSender;
            if (!this.requestCallback.TryGetValue(response.RpcId, out actorMessageSender))
            {
                return;
            }

            this.requestCallback.Remove(response.RpcId);

            Run(actorMessageSender, response);
        }
    }
}
