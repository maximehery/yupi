﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Notification
{
	public class BroadcastNotifMessageComposer : AbstractComposer<string>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, string text)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (text);
				message.AppendString (string.Empty);
				session.Send (message);
			}
		}
	}
}

