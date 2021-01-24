using System;
using System.Collections.Generic;


namespace EGameFrame
{
    public sealed class MasterEntity : Entity
    {
        public Dictionary<Type, List<Entity>> Entities { get; private set; } = new Dictionary<Type, List<Entity>>();
        public List<Component> AllComponents { get; private set; } = new List<Component>();
        //public List<Component> RemoveComponents { get; private set; } = new List<Component>();
        //public List<Component> AddComponents { get; private set; } = new List<Component>();


        public void Update()
        {
            if (AllComponents.Count == 0)
            {
                return;
            }
            for (int i = AllComponents.Count - 1; i >= 0; i--)
            {
                var item = AllComponents[i];
                if (item.IsDisposed)
                {
                    AllComponents.RemoveAt(i);
                    continue;
                }
                item.Update();
            }
        }
    }
}