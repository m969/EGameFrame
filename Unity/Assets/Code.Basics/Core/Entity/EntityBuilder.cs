using System;
using System.Linq;
using System.Collections.Generic;

namespace EGameFrame
{
    public class EntityBuilder<T> where T : Entity, new()
    {
        private List<object> componentInitDatas;


        public EntityBuilder<T> ComponentAwake(object initData)
        {
            componentInitDatas.Add(initData);
            return this;
        }

        public T Create()
        {
            return Entity.Create<T>();
        }
    }
}