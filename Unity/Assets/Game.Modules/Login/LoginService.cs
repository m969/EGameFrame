using System.Collections;
using System.Collections.Generic;
using EGameFrame;

namespace EGameFrame.Login
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