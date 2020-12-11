using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    public Dictionary<string, Module> Modules => new Dictionary<string, Module>();


    private void Start()
    {
        Modules.Add("Map", new Module());
        Modules.Add("Combat", new Module());

        Modules.Add("MapView", new Module());
        Modules.Add("CombatView", new Module());
        Modules.Add("UI", new Module());
    }

    private void Update()
    {

    }
}
