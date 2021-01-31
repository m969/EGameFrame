using System;
using EGameFrame;

namespace ET
{
	public class LocationProxyComponent : Entity
	{
		public static LocationProxyComponent Instance;

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			
			base.Dispose();

			Instance = null;
		}

        public void Awake()
        {
            LocationProxyComponent.Instance = this;
        }

        private long GetLocationSceneId(long key)
        {
            //return StartSceneConfigCategory.Instance.LocationConfig.SceneId;
            return 0;
        }

        public async ETTask Add(long key, long instanceId)
        {
            Log.Info($"location proxy add {key}, {instanceId} {TimeHelper.ServerNow()}");
            //await MessageHelper.CallActor(GetLocationSceneId(key),
            //    new ObjectAddRequest() { Key = key, InstanceId = instanceId });
        }

        public async ETTask Lock(long key, long instanceId, int time = 1000)
        {
            Log.Info($"location proxy lock {key}, {instanceId} {TimeHelper.ServerNow()}");
            //await MessageHelper.CallActor(GetLocationSceneId(key),
            //    new ObjectLockRequest() { Key = key, InstanceId = instanceId, Time = time });
        }

        public async ETTask UnLock(long key, long oldInstanceId, long instanceId)
        {
            Log.Info($"location proxy unlock {key}, {instanceId} {TimeHelper.ServerNow()}");
            //await MessageHelper.CallActor(GetLocationSceneId(key),
            //    new ObjectUnLockRequest() { Key = key, OldInstanceId = oldInstanceId, InstanceId = instanceId });
        }

        public async ETTask Remove(long key)
        {
            Log.Info($"location proxy add {key}, {TimeHelper.ServerNow()}");
            //await MessageHelper.CallActor(GetLocationSceneId(key),
            //    new ObjectRemoveRequest() { Key = key });
        }

        public async ETTask<long> Get(long key)
        {
            if (key == 0)
            {
                throw new Exception($"get location key 0");
            }

            // location server配置到共享区，一个大战区可以配置N多个location server,这里暂时为1
            //ObjectGetResponse response =
            //        (ObjectGetResponse)await MessageHelper.CallActor(GetLocationSceneId(key),
            //            new ObjectGetRequest() { Key = key });
            //return response.InstanceId;
            return 0;
        }

        //public async ETTask AddLocation(this Entity self)
        //{
        //    await LocationProxyComponent.Instance.Add(self.Id, self.InstanceId);
        //}

        //public async ETTask RemoveLocation(this Entity self)
        //{
        //    await LocationProxyComponent.Instance.Remove(self.Id);
        //}
    }
}