using System.Collections;
using System.Collections.Generic;
using EGameFrame;

namespace EGameFrame.Login
{
    public class LoginComponent : Component
    {
        public static LoginComponent Instance { get; set; }

        public override void Setup()
        {
            Instance = this;
        }
    }
}