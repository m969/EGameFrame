using System.Collections;
using System.Collections.Generic;
using EGameFrame;
using EGameFrame.Message;
using EGameFrame.Login;
using EGameFrame.Gate;
using EGameFrame.District;

namespace EGameFrame
{
    public enum GameModule { Login, Gate, Player, Avatar, Space, Monster, Npc }

    public static class DistrictSceneExtension
    {
        public static Module GetModule(this DistrictScene districtScene, GameModule gameModuleType)
        {
            return districtScene.GetModule(gameModuleType.ToString());
        }
    }

    public class GameContext : Entity
    {
        //业务行政场景
        public static Dictionary<DistrictType, DistrictScene> TypeDistrictScenes { get; private set; } = new Dictionary<DistrictType, DistrictScene>();
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
            var loginModule = loginDistrict.AddModule(GameModule.Login.ToString());
            Entity.CreateWithParent<LoginService>(loginModule);

            var gateDistrict = TypeDistrictScenes[DistrictType.Gate];
            var gateModule = loginDistrict.AddModule(GameModule.Gate.ToString());
            Entity.CreateWithParent<GateService>(gateModule);
            var playerModule = loginDistrict.AddModule(GameModule.Player.ToString());
#else
            var clientDistrict = TypeDistrictScenes[DistrictType.Client];

            var loginModule = Entity.CreateWithParent<Module>(clientDistrict);
            Entity.CreateWithParent<LoginService>(loginModule);

            var gateModule = Entity.CreateWithParent<Module>(clientDistrict);
            Entity.CreateWithParent<GateService>(gateModule);
#endif
        }

        public void Update()
        {
        }
    }
}