using System.Collections;
using System.Collections.Generic;
using EGameFrame;
using EGameFrame.Message;

public class GameWorld
{
    //游戏业务模块
    public static Dictionary<string, Module> GameModules => new Dictionary<string, Module>();


    public void Start()
    {
        SetupGameModules();
    }

    private void SetupGameModules()
    {
        var loginModule = Entity.Create<Module>();
        loginModule.AddComponent<LoginComponent>();
        GameModules.Add("LoginModule", loginModule);
    }

    public void Update()
    {
    }
}
