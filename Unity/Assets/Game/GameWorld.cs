using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGameFrame;
using EGameFrame.Message;

public class GameWorld
{
    public static Dictionary<string, Module> Modules => new Dictionary<string, Module>();


    public void Start()
    {
        LogHandler.DebugHandler += LogUtils.Debug;

        EntityFactory.DebugLog = true;
        EntityFactory.Global = new GlobalEntity();

        var loginModule = EntityFactory.Create<Module>();
        loginModule.AddComponent<LoginComponent>();
        var NetOuterComponent = EntityFactory.CreateWithParent<ET.NetOuterComponent>(loginModule);
        Modules.Add("LoginModule", loginModule);

        NetOuterComponent.Awake(NetOuterComponent.Protocol);
        var session = NetOuterComponent.Create("127.0.0.1:20001");
        session.Send(new Monster());

        //Modules.Add("MapModule", new Module());
        //Modules.Add("CombatModule", new Module());

        //Modules.Add("LoginViewModule", new Module());
        //Modules.Add("MapViewModule", new Module());
        //Modules.Add("CombatViewModule", new Module());

    }

    public void Update()
    {
        EntityFactory.Global.Update();
    }
}
