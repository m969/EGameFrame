using System;
using EGameFrame.Login;
using EGameFrame.Message;

namespace EGameFrame.Services.Login
{
    public class LoginRequestHandler
    {
        public void Handle(LoginRequest request, LoginRequest response)
        {
            //LoginService.OnLoginHandle(LoginComponent.Instance);
            var account = EntityFactory.Create<AccountActor>();
            LoginComponent.Instance.IdAccountEntities.Add(account.Id, account);
        }
    }
}
