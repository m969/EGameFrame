using System;
using EGameFrame.Message;
using EGameFrame.Login;
using EGameFrame.Gate;
using ET;

namespace EGameFrame.Services.Gate
{
    public static partial class GateService_Layer
    {
        public static partial async ETTask OnCreatePlayerRequestHandle(this GateService gateService, Session session, LoginRequest request, LoginResponse response)
        {
            Log.Debug($"OnCreatePlayerRequestHandle {request}");
            var player = Entity.CreateWithParent<Player>(gateService);
            var playerActorProxy = player.AddComponent<PlayerActorProxyComponent>();
            gateService.SessionIdPlayers.Add(session.Id, player);
            //var sessionPlayerComponent = session.AddComponent<SessionPlayerComponent>();
            //sessionPlayerComponent.PlayerId = player.Id;
            await ETTask.CompletedTask;
        }
    }
}
