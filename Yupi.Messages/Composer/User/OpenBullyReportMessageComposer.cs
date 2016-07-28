﻿using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.User
{
	public class OpenBullyReportMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (0); // TODO What does 0 mean?
				session.Send (message);
			}
		}
	}
}

