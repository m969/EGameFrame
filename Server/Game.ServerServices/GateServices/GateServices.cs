using System;
using EGameFrame.Message;
using EGameFrame.Login;
using EGameFrame.Gate;
using ET;

namespace EGameFrame.Services.Gate
{
    public static partial class GateServices
    {
        public static partial async ETTask OnCreatePlayerRequestHandle(this GateService gateService, LoginRequest request, LoginResponse response)
        {
            Log.Debug($"OnCreatePlayerRequestHandle {request}");
            var account = EntityFactory.Create<PlayerActorProxy>();
            gateService.IdAccountEntities.Add(account.Id, account);
            await ETTask.CompletedTask;
        }
    }
}
