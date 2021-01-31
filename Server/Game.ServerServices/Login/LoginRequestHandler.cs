using System;
using EGameFrame.Login;
using EGameFrame.Message;
using ET;

namespace EGameFrame.Services.Login
{
    [MessageHandler]
    public class LoginRequestHandler : AMRpcHandler<LoginRequest, LoginResponse>
    {
		protected override async ETTask Run(Session session, LoginRequest request, LoginResponse response, Action reply)
		{
            await LoginService.Instance.OnLoginRequestHandle(session, request, response, reply);
		}
	}
}
