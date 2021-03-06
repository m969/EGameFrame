﻿using System;
using System.Collections.Generic;

namespace EGameFrame
{
    public static class EntityFactory
    {
        public static MasterEntity Master => MasterEntity.Instance;
        public static bool DebugLog { get; set; } = false;
        public static Action<Entity> CreateEntityHandler { get; set; }
        public static Action<Entity> DestroyEntityHandler { get; set; }
        public static Action<Entity> RenameEntityHandler { get; set; }
        public static Action<Entity> AddComponentHandler { get; set; }
        public static Action<Entity> RemoveComponentHandler { get; set; }


        private static T New<T>() where T : Entity, new()
        {
            var entity = new T();
            entity.InstanceId = IdFactory.NewInstanceId();
            if (!Master.Entities.ContainsKey(typeof(T)))
            {
                Master.Entities.Add(typeof(T), new List<Entity>());
            }
            Master.Entities[typeof(T)].Add(entity);
            CreateEntityHandler?.Invoke(entity);
            return entity;
        }

        public static T Create<T>() where T : Entity, new()
        {
            var entity = New<T>();
            entity.Id = entity.InstanceId;
            Master.AddChild(entity);
            entity.Awake();
            if (DebugLog) Log.Debug($"EntityFactory->Create, {typeof(T).Name}={entity.InstanceId}");
            return entity;
        }

        public static T Create<T>(object initData) where T : Entity, new()
        {
            var entity = New<T>();
            entity.Id = entity.InstanceId;
            Master.AddChild(entity);
            entity.Awake(initData);
            if (DebugLog) Log.Debug($"EntityFactory->Create, {typeof(T).Name}={entity.InstanceId}, {initData}");
            return entity;
        }

        public static T CreateWithParent<T>(Entity parent) where T : Entity, new()
        {
            var entity = New<T>();
            entity.Id = entity.InstanceId;
            parent.AddChild(entity);
            entity.Awake();
            if (DebugLog) Log.Debug($"EntityFactory->CreateWithParent, {parent.GetType().Name}, {typeof(T).Name}={entity.InstanceId}");
            return entity;
        }

        public static T CreateWithParent<T>(Entity parent, object initData) where T : Entity, new()
        {
            var entity = New<T>();
            entity.Id = entity.InstanceId;
            parent.AddChild(entity);
            entity.Awake(initData);
            if (DebugLog) Log.Debug($"EntityFactory->CreateWithParent, {parent.GetType().Name}, {typeof(T).Name}={entity.InstanceId}");
            return entity;
        }
    }

    public abstract partial class Entity
    {
        private static MasterEntity Master => EntityFactory.Master;


        public static EntityBuilder<T> Build<T>() where T : Entity, new()
        {
            return new EntityBuilder<T>();
        }

        public static T Create<T>() where T : Entity, new()
        {
            return EntityFactory.Create<T>();
        }

        public static T Create<T>(object initData) where T : Entity, new()
        {
            return EntityFactory.Create<T>(initData);
        }

        public static T CreateWithParent<T>(Entity parent) where T : Entity, new()
        {
            return EntityFactory.CreateWithParent<T>(parent);
        }

        public static T CreateWithParent<T>(Entity parent, object initData) where T : Entity, new()
        {
            return EntityFactory.CreateWithParent<T>(parent, initData);
        }

        public static void Destroy(Entity entity)
        {
            entity.Dispose();
            entity.OnDestroy();
            EntityFactory.DestroyEntityHandler?.Invoke(entity);
        }
    }
}