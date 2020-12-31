using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGameFrame;

public class UnityApp : MonoBehaviour
{
    public static MainApp MainApp { get; private set; }
    public static UnitySDK UnitySDK { get; private set; }


    private void Start()
    {
        MainApp = new MainApp();
        MainApp.Start();

        UnitySDK = new UnitySDK();
        UnitySDK.Start();

        //GameWorld = new GameWorld();
        //GameWorld.Start();

        //Samples.SchemaFilesExample2.SchemaFilesExample2.Run();
    }

    private void Update()
    {
        MainApp.Update();
        UnitySDK.Update();
    }
}
