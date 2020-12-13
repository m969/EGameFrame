using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using EGamePlay;

namespace ServerApp
{
    class Program
    {
        private static Dictionary<string, Module> Modules = new Dictionary<string, Module>();


        static void Main(string[] args)

        {
            Console.WriteLine("ServerApp Program start...");

            LogHandler.DebugHandler = (log) => { Console.WriteLine(log); };

            EntityFactory.DebugLog = true;

            EntityFactory.Global = new GlobalEntity();

            var loginModule = EntityFactory.Create<Module>();
            loginModule.AddComponent<LoginComponent>();
            Modules.Add("LoginModule", loginModule);

            Samples.MonsterAttributeExample.MonsterAttributeExample.Run();

            Update().Coroutine();
        }

        private static async ET.ETVoid Update()
        {
            while (true)
            {
                Thread.Sleep(1);
                EntityFactory.Global.Update();
            }
        }
    }
}
