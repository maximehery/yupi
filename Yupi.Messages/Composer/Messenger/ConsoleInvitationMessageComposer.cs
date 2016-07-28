﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
	public class ConsoleInvitationMessageComposer : AbstractComposer
	{
		public void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int habboId, string content)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (habboId);
				message.AppendString(content);
				session.Send (message);
			}
		}
	}
}

