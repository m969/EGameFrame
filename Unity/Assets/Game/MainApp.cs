using System.Collections.Generic;
using EGameFrame.Message;
using EGameFrame;
using ET;

namespace EGameFrame
{
    public class MainApp
    {
        public GameWorld GameWorld { get; private set; }
        public object ECSWorld { get; private set; }
        public object IoCContainer { get; private set; }
        public object PhysicsWorld { get; private set; }
        //程序基础模块
        public Dictionary<string, Module> CodeModules { get; private set; } = new Dictionary<string, Module>();


        // Start is called before the first frame update
        public void Start()
        {
#if !SERVER
            LogHandler.DebugHandler += LogUtils.Debug;
            LogHandler.ErrorHandler += LogUtils.Error;
            LogHandler.ExceptionHandler += LogUtils.LogException;
            Entity.IsServer = false;
#else 
            LogHandler.DebugHandler += (log)=> { System.Console.WriteLine(log); };
            LogHandler.ErrorHandler += (log)=> { System.Console.WriteLine(log); };
            LogHandler.ExceptionHandler += (log)=> { System.Console.WriteLine(log); };
            Entity.IsServer = true;
#endif
            EntityFactory.DebugLog = true;
            EntityFactory.Master = new MasterEntity();

            SetupCodeModules();

            GameWorld = new GameWorld();
            GameWorld.Start();
        }

        private void SetupCodeModules()
        {
            var sessionModule = Entity.Create<Module>();
            CodeModules.Add("SessionModule", sessionModule);
            EntityFactory.CreateWithParent<OpcodeTypeComponent>(sessionModule);
            EntityFactory.CreateWithParent<NetOuterComponent>(sessionModule);
        }

        // Update is called once per frame
        public void Update()
        {
            EntityFactory.Master.Update();
        }
    }
}