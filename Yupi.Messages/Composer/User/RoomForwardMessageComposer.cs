﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class RoomForwardMessageComposer : AbstractComposer<int>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int roomId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (roomId);
				session.Send (message);
			}
		}
	}
}

