using System;
using EGameFrame.Message;
using EGameFrame.Login;
using ET;

namespace EGameFrame.Services.Login
{
    public static partial class LoginService_Layer
    {
        public static partial ETTask OnLoginRequestHandle(this LoginService loginService, Session session, LoginRequest request, LoginResponse response, Action reply);
    }
}
