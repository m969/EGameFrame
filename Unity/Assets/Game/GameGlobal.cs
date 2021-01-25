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
            SetupGameModules();

#if SERVER
            TypeDistrictScenes.Add(DistrictType.Gate, Entity.Create<DistrictScene>());
            TypeDistrictScenes.Add(DistrictType.World, Entity.Create<DistrictScene>());
            TypeDistrictScenes.Add(DistrictType.DB, Entity.Create<DistrictScene>());
#else
            TypeDistrictScenes.Add(DistrictType.Client, Entity.Create<DistrictScene>());
#endif
        }

        private void SetupGameModules()
        {
#if SERVER
            var gateDistrict = TypeDistrictScenes[DistrictType.Gate];
            var loginModule = Entity.CreateWithParent<Module>(gateDistrict);
            loginModule.AddComponent<LoginComponent>();
#else
            var clientDistrict = TypeDistrictScenes[DistrictType.Client];
            var loginModule = Entity.CreateWithParent<Module>(clientDistrict);
            loginModule.AddComponent<LoginComponent>();
#endif
            //GameModules.Add("LoginModule", loginModule);
        }

        public void Update()
        {
        }
    }
}