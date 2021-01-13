using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using EGameFrame;
using EGameFrame.Message;
using ET;

namespace ServerApp
{
    class Program
    {
        public static MainApp MainApp { get; private set; }


        static void Main(string[] args)
        {
            Console.WriteLine("ServerApp Program start...");

            try
            {
                MainApp = new MainApp();
                MainApp.Start();

                var netOuterComponent = MainApp.CodeModules["SessionModule"].GetTypeChildren<NetOuterComponent>()[0] as NetOuterComponent;
                netOuterComponent.Awake(netOuterComponent.Protocol);
                var session = netOuterComponent.Create("127.0.0.1:20001");
                session.Send(new Monster());

                while (true)
                {
                    Thread.Sleep(1);
                    MainApp.Update();
                }
            }
            catch (Exception e)
            {
                EGameFrame.Log.Exception(e);
            }
        }
    }
}
