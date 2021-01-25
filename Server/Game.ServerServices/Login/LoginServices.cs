using System;
using EGameFrame.Message;
using EGameFrame.Login;
using ET;

namespace EGameFrame.Services.Login
{
    public static partial class LoginServices
    {
        public static partial ETTask OnLoginRequestHandle(this LoginComponent loginComponent, LoginRequest request, LoginRequest response);
    }
}
