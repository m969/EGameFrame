using System;
using EGameFrame.Login;
using EGameFrame.Gate;
using ET;
using EGameFrame.Message;

namespace EGameFrame.Services.Gate
{
    /// <summary>
    /// Player业务层
    /// </summary>
    public static partial class Player_Layer
    {
        public static partial async ETTask OnSendChatTextRequestHandle(this Player player, LoginRequest request, LoginResponse response)
        {
            player.Client.OnReceiveChatText("");
            await ETTask.CompletedTask;
        }
    }
}
