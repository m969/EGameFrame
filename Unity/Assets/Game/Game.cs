using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay;

public class Game
{
    public static Dictionary<string, Module> Modules => new Dictionary<string, Module>();


    public void Start()
    {
        LogHandler.DebugHandler += LogUtils.Debug;

        EntityFactory.DebugLog = true;
        EntityFactory.Global = new GlobalEntity();

        var loginModule = EntityFactory.Create<Module>();
        loginModule.AddComponent<LoginComponent>();
        Modules.Add("LoginModule", loginModule);


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
