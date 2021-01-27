using System;
using EGameFrame.Message;
using EGameFrame.Login;
using ET;

namespace EGameFrame.Services.Gate
{
    public static partial class GateServices
    {
        public static partial ETTask OnLoginRequestHandle(this LoginComponent loginComponent, LoginRequest request, LoginResponse response);
    }
}
