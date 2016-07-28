﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class SendMonsterplantIdMessageComposer : AbstractComposer<uint>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, uint entityId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (entityId);
				session.Send (message);
			}
		}
	}
}

