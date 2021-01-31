using System.Collections.Generic;
using System.Threading;
using EGameFrame.District;
using EGameFrame.Message;
using ET;

namespace EGameFrame
{
    public enum DistrictType { Login, Gate, Spaces, DB, Client, }
    public enum CodeModule { NetworkMessage }

    public class MainApp
    {
        public GameGlobal GameGlobal { get; private set; }
        public object ECSWorld { get; private set; }
        public object IoCContainer { get; private set; }
        public object PhysicsWorld { get; private set; }
        //程序基础模块
        public Dictionary<CodeModule, Module> CodeModules { get; private set; } = new Dictionary<CodeModule, Module>();


        // Start is called before the first frame update
        public void Start()
        {
            // 异步方法全部会回掉到主线程
            SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);
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
            AssemblyHelper.Add(typeof(EGameFrame.Services.UniversalVirtualContainer).Assembly);
#endif
            AssemblyHelper.Add(typeof(FlatBuffersSerializeHelper).Assembly);

            MessageSerializeHelper.SerializeToAction = FlatBuffersSerializeHelper.SerializeTo;
            MessageSerializeHelper.DeserializeFromFunc2 = FlatBuffersSerializeHelper.DeserializeFrom;

            EntityFactory.DebugLog = true;
            MasterEntity.Create();
            Entity.Create<TimerComponent>();

            InstallModules();

            GameGlobal = Entity.Create<GameGlobal>();
        }

        private NetOuterComponent netOuterComponent;
        private void InstallModules()
        {
            var sessionModule = Entity.Create<Module>();
            CodeModules.Add(CodeModule.NetworkMessage, sessionModule);
            Entity.CreateWithParent<OpcodeTypeComponent>(sessionModule).Load();
            netOuterComponent = Entity.CreateWithParent<NetOuterComponent>(sessionModule);
            sessionModule.AddComponent<MessageDispatcherComponent>().Load();
        }

        // Update is called once per frame
        public void Update()
        {
            try
            {
                OneThreadSynchronizationContext.Instance.Update();
                EntityFactory.Master.Update();
                TimerComponent.Instance.Update();
                netOuterComponent.Update();
            }
            catch (System.Exception e)
            {
                EGameFrame.Log.Exception(e);
            }
        }
    }
}