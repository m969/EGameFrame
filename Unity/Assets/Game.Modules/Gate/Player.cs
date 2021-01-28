using System.Collections;
using System.Collections.Generic;

namespace EGameFrame.Gate
{
    public partial class Player : Entity
    {
        public PlayerActorProxy PlayerActorProxy { get; set; }
        public long UnitId { get; set; }
        public int ClientMessageType = 0;
        public PlayerActorProxy AllClients
        {
            get
            {
                ClientMessageType = 2;
                return PlayerActorProxy;
            }
        }
        public PlayerActorProxy OtherClients
        {
            get
            {
                ClientMessageType = 1;
                return PlayerActorProxy;
            }
        }
        public PlayerActorProxy Client
        {
            get
            {
                ClientMessageType = 0;
                return PlayerActorProxy;
            }
        }
    }
}