using System;
using EGameFrame.Message;
using EGameFrame.Login;
using EGameFrame.Gate;
using EGameFrame.Services.Gate;
using ET;

namespace EGameFrame.Services.Login
{
    public static partial class LoginService_Layer
    {
        public static partial async ETTask OnLoginRequestHandle(this LoginService loginService, Session session, LoginRequest request, LoginResponse response, Action reply)
        {
            //var account = EntityFactory.Create<AccountActor>();
            //GateService.Instance.IdAccountEntities.Add(account.Id, account);
            Log.Debug($"OnLoginRequestHandle {request}");
            await GateService.Instance.OnCreatePlayerRequestHandle(session, request, response);
            reply();
            await ETTask.CompletedTask;
        }
    }
}
