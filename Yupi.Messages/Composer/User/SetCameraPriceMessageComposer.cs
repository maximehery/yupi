﻿using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class SetCameraPriceMessageComposer : AbstractComposer
	{
		public void Compose(Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int credits, int seasonalCurrency) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (credits);
				message.AppendInteger (seasonalCurrency);
				session.Send (message);
			}
		}
	}
}

