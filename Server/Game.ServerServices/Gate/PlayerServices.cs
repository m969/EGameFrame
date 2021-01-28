using System;
using EGameFrame.Message;
using EGameFrame.Login;
using EGameFrame.Gate;
using ET;

namespace EGameFrame.Services.Gate
{
    public static partial class PlayerServices
    {
        public static partial ETTask OnSendChatTextRequestHandle(this Player player, LoginRequest request, LoginResponse response);
    }
}
