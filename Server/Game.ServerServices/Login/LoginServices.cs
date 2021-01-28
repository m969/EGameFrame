using System;
using EGameFrame.Message;
using EGameFrame.Login;
using ET;

namespace EGameFrame.Services.Login
{
    public static partial class LoginServices
    {
        public static partial ETTask OnLoginRequestHandle(this LoginService loginService, LoginRequest request, LoginResponse response);
    }
}
