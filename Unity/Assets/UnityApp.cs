using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGameFrame;
using ET;
using EGameFrame.Message;

public class UnityApp : MonoBehaviour
{
    public static MainApp MainApp { get; private set; }
    public static UnitySDK UnitySDK { get; private set; }


    private async void Start()
    {
        try
        {
            MainApp = new MainApp();
            MainApp.Start();

            UnitySDK = new UnitySDK();
            UnitySDK.Start();

            var netOuterComponent = MainApp.CodeModules["SessionModule"].GetTypeChildren<NetOuterComponent>()[0] as NetOuterComponent;
            netOuterComponent.Awake(netOuterComponent.Protocol);
            var session = netOuterComponent.Create("127.0.0.1:20001");
            await TimerComponent.Instance.WaitAsync(1000);
            session.Send(new LoginRequest());
        }
        catch (System.Exception e)
        {
            EGameFrame.Log.Exception(e);
        }
    }

    private void Update()
    {
        try
        {
            MainApp.Update();
            UnitySDK.Update();
        }
        catch (System.Exception e)
        {
            EGameFrame.Log.Exception(e);
        }
    }
}
