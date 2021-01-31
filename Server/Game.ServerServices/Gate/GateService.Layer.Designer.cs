using System;
using EGameFrame.Message;
using EGameFrame.Login;
using EGameFrame.Gate;
using ET;

namespace EGameFrame.Services.Gate
{
    public static partial class GateService_Layer
    {
        public static partial ETTask OnCreatePlayerRequestHandle(this GateService gateService, Session session, LoginRequest request, LoginResponse response);
    }
}
