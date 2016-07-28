﻿using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class LoadCatalogClubGiftsMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(0); // i
				message.AppendInteger(0); // i2
				message.AppendInteger(1); // TODO Magic constants
				session.Send (message);
			}
		}
	}
}

