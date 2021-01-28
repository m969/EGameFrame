using System;
using EGameFrame.Message;
using EGameFrame.Login;
using EGameFrame.Gate;
using ET;

namespace EGameFrame.Services.Gate
{
    public static partial class GateServices
    {
        public static partial ETTask OnCreatePlayerRequestHandle(this GateService gateService, LoginRequest request, LoginResponse response);
    }
}
