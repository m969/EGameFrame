using System.Collections;
using System.Collections.Generic;
using EGameFrame;

namespace EGameFrame.Login
{
    public class LoginComponent : Component
    {
        public static LoginComponent Instance { get; set; }
        public Dictionary<long, AccountActor> IdAccountEntities { get; set; } = new Dictionary<long, AccountActor>();
        public Dictionary<long, Player> IdPlayers { get; set; } = new Dictionary<long, Player>();

        public override void Setup()
        {
            Instance = this;
        }
    }
}