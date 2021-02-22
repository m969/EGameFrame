using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using EGameFrame;
using EGameFrame.Message;
using EGameFrame.Gate;
using EGameFrame.Services.Login;
using EGameFrame.Services.Gate;
using ET;

namespace ServerApp
{
    class Program
    {
        public static AppContext AppContext { get; private set; }


        static void Main(string[] args)
        {
            System.Console.WriteLine("ServerApp Program start...");

            try
            {
                AppContext = new AppContext();
                AppContext.Start();

                var module = AppContext.CodeModules[CodeModule.NetworkMessage];
                var netOuterComponent = module.GetTypeChildren<NetOuterComponent>()[0] as NetOuterComponent;
                netOuterComponent.Awake(netOuterComponent.Protocol, "127.0.0.1:20001");
                var outerMessageDispatcher = netOuterComponent.MessageDispatcher as OuterMessageDispatcher;

                outerMessageDispatcher.OnSessionMessageDispatchAction = async (session, opcode, message) => {
                    switch (message)
                    {
                        case IActorLocationRequest actorLocationRequest: // gate session收到actor rpc消息，先向actor 发送rpc请求，再将请求结果返回客户端
                            {
                                //var playerId = session.GetComponent<SessionPlayerComponent>().PlayerId;

                                //var gateDisctrict = GameGlobal.TypeDistrictScenes[DistrictType.Gate];
                                //var gateModule = gateDisctrict.GetModule(GameModule.Gate);
                                //var playerModule = gateDisctrict.GetModule(GameModule.Player);
                                //var player = playerModule.GetChildren<Player>(playerId);
                                var rpcId = actorLocationRequest.RpcId; // 这里要保存客户端的rpcId
                                var instanceId = session.InstanceId;
                                var entityType = opcode / 1000;
                                if (entityType == 0)
                                {
                                    var player = GateService.Instance.SessionIdPlayers[session.Id];
                                    var response = new LoginResponse();
                                    await player.OnSendChatTextRequestHandle((LoginRequest)actorLocationRequest, response);
                                    response.RpcId = rpcId;
                                    // session可能已经断开了，所以这里需要判断
                                    if (session.InstanceId == instanceId)
                                    {
                                        session.Reply(response);
                                    }
                                }

                                //long unitId = session.GetComponent<SessionPlayerComponent>().Player.UnitId;
                                //int rpcId = actorLocationRequest.RpcId; // 这里要保存客户端的rpcId
                                //long instanceId = session.InstanceId;
                                //IResponse response = await ActorLocationSenderComponent.Instance.Call(unitId, actorLocationRequest);
                                //response.RpcId = rpcId;
                                //// session可能已经断开了，所以这里需要判断
                                //if (session.InstanceId == instanceId)
                                //{
                                //    session.Reply(response);
                                //}
                                break;
                            }
                        case IActorLocationMessage actorLocationMessage:
                            {
                                //long unitId = session.GetComponent<SessionPlayerComponent>().Player.UnitId;
                                //ActorLocationSenderComponent.Instance.Send(unitId, actorLocationMessage);
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
            catch (System.Exception e)
            {
                EGameFrame.Log.Exception(e);
            }

            while (true)
            {
                Thread.Sleep(1);
                AppContext.Update();
            }
        }
    }
}
