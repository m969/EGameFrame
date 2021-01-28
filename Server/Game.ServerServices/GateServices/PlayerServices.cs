using System;
using EGameFrame.Login;
using EGameFrame.Gate;
using ET;
using EGameFrame.Message;

namespace EGameFrame.Services.Gate
{
    public static partial class PlayerServices
    {
        public static partial async ETTask OnSendChatTextRequestHandle(this Player player, LoginRequest request, LoginResponse response)
        {
            player.Client.OnReceiveChatText("");
            await ETTask.CompletedTask;
        }
    }
}
