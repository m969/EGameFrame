using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EGameFrame
{
    public class MainApp
    {
        public GameWorld GameWorld { get; private set; }
        public object ECSWorld { get; private set; }
        public object IoCContainer { get; private set; }
        public object PhysicsWorld { get; private set; }


        // Start is called before the first frame update
        public void Start()
        {
            GameWorld = new GameWorld();
        }

        // Update is called once per frame
        public void Update()
        {

        }

        //public T CreateWorld<T>() where T : new()
        //{
        //    GameWorld = new T();
        //    return (T)GameWorld;
        //}

        //public T GetWorld<T>() where T : new()
        //{
        //    return (T)GameWorld;
        //}
    }
}