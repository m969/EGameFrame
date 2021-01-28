﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using EGameFrame;
using EGameFrame.Message;
using ET;

namespace ServerApp
{
    class Program
    {
        public static MainApp MainApp { get; private set; }


        static void Main(string[] args)
        {
            Console.WriteLine("ServerApp Program start...");

            try
            {
                MainApp = new MainApp();
                MainApp.Start();

                var module = MainApp.CodeModules[CodeModule.NetworkMessage];
                var netOuterComponent = module.GetTypeChildren<NetOuterComponent>()[0] as NetOuterComponent;
                netOuterComponent.Awake(netOuterComponent.Protocol, "127.0.0.1:20001");
                var outerMessageDispatcher = netOuterComponent.MessageDispatcher as OuterMessageDispatcher;

                outerMessageDispatcher.OnSessionMessageDispatchAction = async (session, opcode, message) => {
                    //根据消息接口判断是不是Actor消息，不同的接口做不同的处理
                    switch (message)
                    {
                        //case IActorLocationRequest actorLocationRequest: // gate session收到actor rpc消息，先向actor 发送rpc请求，再将请求结果返回客户端
                        //    {
                        //        long unitId = session.GetComponent<SessionPlayerComponent>().Player.UnitId;
                        //        int rpcId = actorLocationRequest.RpcId; // 这里要保存客户端的rpcId
                        //        long instanceId = session.InstanceId;
                        //        IResponse response = await ActorLocationSenderComponent.Instance.Call(unitId, actorLocationRequest);
                        //        response.RpcId = rpcId;
                        //        // session可能已经断开了，所以这里需要判断
                        //        if (session.InstanceId == instanceId)
                        //        {
                        //            session.Reply(response);
                        //        }
                        //        break;
                        //    }
                        //case IActorLocationMessage actorLocationMessage:
                        //    {
                        //        long unitId = session.GetComponent<SessionPlayerComponent>().Player.UnitId;
                        //        ActorLocationSenderComponent.Instance.Send(unitId, actorLocationMessage);
                        //        break;
                        //    }
                        case IActorRequest actorRequest:  // 分发IActorRequest消息，目前没有用到，需要的自己添加
                            {
                                break;
                            }
                        case IActorMessage actorMessage:  // 分发IActorMessage消息，目前没有用到，需要的自己添加
                            {
                                break;
                            }
                        default:
                            {
                                // 非Actor消息
                                MessageDispatcherComponent.Instance.Handle(session, new MessageInfo(opcode, message));
                                break;
                            }
                    }
                };
            }
            catch (Exception e)
            {
                EGameFrame.Log.Exception(e);
            }

            while (true)
            {
                Thread.Sleep(1);
                MainApp.Update();
            }
        }
    }
}
