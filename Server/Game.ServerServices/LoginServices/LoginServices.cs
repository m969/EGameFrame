using System;
using EGameFrame.Message;
using EGameFrame.Login;
using EGameFrame.Gate;
using EGameFrame.Services.Gate;
using ET;

namespace EGameFrame.Services.Login
{
    public static partial class LoginServices
    {
        public static partial async ETTask OnLoginRequestHandle(this LoginService loginService, LoginRequest request, LoginResponse response)
        {
            //var account = EntityFactory.Create<AccountActor>();
            //GateService.Instance.IdAccountEntities.Add(account.Id, account);
            Log.Debug($"OnLoginRequestHandle {request}");
            await GateService.Instance.OnCreatePlayerRequestHandle(request, response);
            await ETTask.CompletedTask;
        }
    }
}
