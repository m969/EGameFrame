using System;
using EGameFrame.Message;
using EGameFrame.Login;
using ET;

namespace EGameFrame.Services.Gate
{
    public static partial class GateServices
    {
        public static partial async ETTask OnLoginRequestHandle(this LoginComponent loginComponent, LoginRequest request, LoginResponse response)
        {
            var account = EntityFactory.Create<AccountActor>();
            loginComponent.IdAccountEntities.Add(account.Id, account);
            await ETTask.CompletedTask;
        }
    }
}
