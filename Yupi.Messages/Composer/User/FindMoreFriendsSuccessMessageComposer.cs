﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class FindMoreFriendsSuccessMessageComposer : AbstractComposer<bool>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, bool success)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool (success);
				session.Send (message);
			}
		}
	}
}

