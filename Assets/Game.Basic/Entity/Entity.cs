﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EGamePlay
{
    public abstract partial class Entity
    {
        public static T Create<T>() where T : Entity, new()
        {
            return EntityFactory.Create<T>();
        }

        public static void Destroy(Entity entity)
        {
            entity.OnDestroy();
            entity.Dispose();
        }
    }
    public abstract partial class Entity : IDisposable
    {
#if !SERVER
        public UnityEngine.GameObject GameObject { get; set; }
#endif
        public long Id { get; set; }
        public long InstanceId { get; set; }
        private GlobalEntity Global => EntityFactory.Global;
        private Entity parent;
        public Entity Parent { get { return parent; } private set { parent = value; OnSetParent(value); } }
        public bool IsDisposed { get { return InstanceId == 0; } }
        public Dictionary<Type, Component> Components { get; set; } = new Dictionary<Type, Component>();
        private List<Entity> Children { get; set; } = new List<Entity>();
        private Dictionary<Type, List<Entity>> Type2Children { get; set; } = new Dictionary<Type, List<Entity>>();


        public Entity()
        {
#if !SERVER
            GameObject = new UnityEngine.GameObject(GetType().Name);
#endif
        }

        public virtual void Awake()
        {

        }

        public virtual void Awake(object initData)
        {

        }

        public virtual void OnDestroy()
        {

        }

        public virtual void Dispose()
        {
            if (EntityFactory.DebugLog) Log.Debug($"{GetType().Name}->Dispose");
            if (Children.Count > 0)
            {
                for (int i = Children.Count - 1; i >= 0; i--)
                {
                    Entity.Destroy(Children[i]);
                }
                Children.Clear();
                Type2Children.Clear();
            }

            foreach (Component component in this.Components.Values)
            {
                component.Dispose();
            }
            this.Components.Clear();
            Parent?.RemoveChild(this);
            InstanceId = 0;
#if !SERVER
            UnityEngine.GameObject.Destroy(GameObject);
#endif
        }

        public virtual void OnSetParent(Entity parent)
        {

        }

        public T GetParent<T>() where T : Entity
        {
            return parent as T;
        }

        public T AddComponent<T>() where T : Component, new()
        {
            var c = new T();
            c.Entity = this;
            c.IsDisposed = false;
            this.Components.Add(typeof(T), c);
            Global.AddComponents.Add(c);
            if (EntityFactory.DebugLog) Log.Debug($"{GetType().Name}->AddComponent, {typeof(T).Name}");
            c.Setup();
            return c;
        }

        public void RemoveComponent<T>() where T : Component
        {
            this.Components[typeof(T)].OnDestroy();
            this.Components[typeof(T)].Dispose();
            this.Components.Remove(typeof(T));
        }

        public T GetComponent<T>() where T : Component
        {
            if (this.Components.TryGetValue(typeof(T),  out var component))
            {
                return component as T;
            }
            return null;
        }

        public void SetParent(Entity parent)
        {
            Parent?.RemoveChild(this);
            parent?.AddChild(this);
        }

        public void AddChild(Entity child)
        {
            Children.Add(child);
            if (!Type2Children.ContainsKey(child.GetType()))
            {
                Type2Children.Add(child.GetType(), new List<Entity>());
            }
            Type2Children[child.GetType()].Add(child);
            child.Parent = this;
#if !SERVER
            child.GameObject.transform.SetParent(GameObject.transform);
#endif
        }

        public void RemoveChild(Entity child)
        {
            Children.Remove(child);
            if (Type2Children.ContainsKey(child.GetType()))
            {
                Type2Children[child.GetType()].Remove(child);
            }
            child.Parent = null;
#if !SERVER
            child.GameObject.transform.SetParent(null);
#endif
        }

        public Entity[] GetChildren()
        {
            return Children.ToArray();
        }

        public Entity[] GetTypeChildren<T>() where T : Entity
        {
            return Type2Children[typeof(T)].ToArray();
        }

        public T Publish<T>(T TEvent) where T : class
        {
            var eventComponent = GetComponent<EventComponent>();
            if (eventComponent == null)
            {
                eventComponent = AddComponent<EventComponent>();
            }
            eventComponent.Publish(TEvent);
            return TEvent;
        }

        public EventSubscribe<T> Subscribe<T>(Action<T> action) where T : class
        {
            var eventComponent = GetComponent<EventComponent>();
            if (eventComponent == null)
            {
                eventComponent = AddComponent<EventComponent>();
            }
            return eventComponent.Subscribe(action);
        }

        public void UnSubscribe<T>(Action<T> action) where T : class
        {
            var eventComponent = GetComponent<EventComponent>();
            if (eventComponent != null)
            {
                eventComponent.UnSubscribe(action);
            }
        }
    }
}