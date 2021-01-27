namespace ET
{
	/// <summary>
	/// actor RPC消息响应
	/// </summary>
	[Message(1)]
	public class ActorResponse : IActorLocationResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }
	}
}
