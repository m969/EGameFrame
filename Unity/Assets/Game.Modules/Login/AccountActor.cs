using System.Collections;
using System.Collections.Generic;
using EGameFrame;
using ET;

namespace EGameFrame.Login
{
    public class AccountActor : Entity
    {
        public Session GateSession { get; set; }
        public Session WorldSession { get; set; }
    }
}