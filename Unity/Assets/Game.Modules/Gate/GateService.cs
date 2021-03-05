using System.Collections;
using System.Collections.Generic;
using EGameFrame;

namespace Game.Modules.Gate
{
    public class GateService : Entity
    {
        public static GateService Instance { get; set; }
        public Dictionary<long, Player> IdPlayers { get; set; } = new Dictionary<long, Player>();
        public Dictionary<long, Player> SessionIdPlayers { get; set; } = new Dictionary<long, Player>();

        public override void Awake()
        {
            Instance = this;
        }
    }
}