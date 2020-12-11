using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityApp : MonoBehaviour
{
    public static Game Game { get; private set; }
    public static UnitySDK UnitySDK { get; private set; }


    private void Start()
    {
        Game = new Game();
        UnitySDK = new UnitySDK();
    }

    private void Update()
    {
        
    }
}
