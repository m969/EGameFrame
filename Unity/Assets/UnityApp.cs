using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityApp : MonoBehaviour
{
    public static Game Game { get; private set; }
    public static UnitySDK UnitySDK { get; private set; }


    private void Start()
    {
        UnitySDK = new UnitySDK();
        UnitySDK.Start();

        Game = new Game();
        Game.Start();

        //Samples.SchemaFilesExample2.SchemaFilesExample2.Run();
    }

    private void Update()
    {
        
    }
}
