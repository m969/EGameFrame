using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using ET;
using EGameFrame.Message;

public class UnityApp : MonoBehaviour
{
    public static AppContext AppContext { get; private set; }
    public static UnitySDK UnitySDK { get; private set; }


    private async void Start()
    {
        try
        {
            AppContext = new AppContext();
            AppContext.Start();

            UnitySDK = new UnitySDK();
            UnitySDK.Start();

            var netOuterComponent = AppContext.CodeModules[CodeModule.NetworkMessage].GetTypeChildren<NetOuterComponent>()[0] as NetOuterComponent;
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
            AppContext.Update();
            UnitySDK.Update();
        }
        catch (System.Exception e)
        {
            EGameFrame.Log.Exception(e);
        }
    }
}
