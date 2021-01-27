using System;
using EGameFrame;

namespace ET
{
	public interface IMActorHandler
	{
		ETTask Handle(Session session, Entity entity, object actorMessage);
		Type GetMessageType();
	}
}