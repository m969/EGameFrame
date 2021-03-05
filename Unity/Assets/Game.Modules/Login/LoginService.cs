using System.Collections;
using System.Collections.Generic;
using EGameFrame;

namespace Game.Modules.Login
{
    public class LoginService : Entity
    {
        public static LoginService Instance { get; set; }

        public override void Awake()
        {
            Instance = this;
        }
    }
}