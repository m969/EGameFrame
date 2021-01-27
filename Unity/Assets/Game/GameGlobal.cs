using System.Collections;
using System.Collections.Generic;
using EGameFrame;
using EGameFrame.Message;
using EGameFrame.Login;
using EGameFrame.District;

namespace EGameFrame
{
    public class GameGlobal : Entity
    {
        //业务行政场景
        public Dictionary<DistrictType, DistrictScene> TypeDistrictScenes { get; private set; } = new Dictionary<DistrictType, DistrictScene>();
        //游戏业务模块
        public static Dictionary<string, Module> GameModules { get; private set; } = new Dictionary<string, Module>();


        public override void Awake()
        {
#if SERVER
            TypeDistrictScenes.Add(DistrictType.Login, Entity.Create<DistrictScene>());
            TypeDistrictScenes.Add(DistrictType.Gate, Entity.Create<DistrictScene>());
            TypeDistrictScenes.Add(DistrictType.Spaces, Entity.Create<DistrictScene>());
            TypeDistrictScenes.Add(DistrictType.DB, Entity.Create<DistrictScene>());
#else
            TypeDistrictScenes.Add(DistrictType.Client, Entity.Create<DistrictScene>());
#endif

            InstallModules();
        }

        private void InstallModules()
        {
#if SERVER
            var loginDistrict = TypeDistrictScenes[DistrictType.Login];
            var loginModule = Entity.CreateWithParent<Module>(loginDistrict);
            loginModule.AddComponent<LoginComponent>();
#else
            var clientDistrict = TypeDistrictScenes[DistrictType.Client];
            var loginModule = Entity.CreateWithParent<Module>(clientDistrict);
            loginModule.AddComponent<LoginComponent>();
#endif
        }

        public void Update()
        {
        }
    }
}