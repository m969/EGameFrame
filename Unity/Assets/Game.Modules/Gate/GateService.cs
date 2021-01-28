using System.Collections;
using System.Collections.Generic;
using EGameFrame;

namespace EGameFrame.Gate
{
    public class GateService : Entity
    {
        public static GateService Instance { get; set; }
        public Dictionary<long, PlayerActorProxy> IdAccountEntities { get; set; } = new Dictionary<long, PlayerActorProxy>();
        public Dictionary<long, Player> IdPlayers { get; set; } = new Dictionary<long, Player>();

        public override void Awake()
        {
            Instance = this;
        }
    }
}