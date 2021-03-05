using System.Collections;
using System.Collections.Generic;
using EGameFrame;

namespace Game.Modules.Gate
{
    public partial class Player : Entity
    {
        public PlayerActorProxyComponent PlayerActorProxy { get; set; }
        public long UnitId { get; set; }
        public int ClientMessageType = 0;
        public PlayerActorProxyComponent AllClients
        {
            get
            {
                ClientMessageType = 2;
                return PlayerActorProxy;
            }
        }
        public PlayerActorProxyComponent OtherClients
        {
            get
            {
                ClientMessageType = 1;
                return PlayerActorProxy;
            }
        }
        public PlayerActorProxyComponent Client
        {
            get
            {
                ClientMessageType = 0;
                return PlayerActorProxy;
            }
        }
    }
}