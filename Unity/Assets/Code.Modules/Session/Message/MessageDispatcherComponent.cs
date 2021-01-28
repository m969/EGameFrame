using System.Collections.Generic;
using EGameFrame;

namespace ET
{
	/// <summary>
	/// 消息分发组件
	/// </summary>
	public class MessageDispatcherComponent : Component
	{
		public static MessageDispatcherComponent Instance { get; set; }
		public readonly Dictionary<ushort, List<IMHandler>> Handlers = new Dictionary<ushort, List<IMHandler>>();

        public override void Setup()
        {
			Instance = this;
			Log.Debug($"MessageDispatcherComponent->Setup {Instance}");
		}
	}
}