﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
	public class ThumbnailSuccessMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(true);
				message.AppendBool(false); // TODO Hardcoded
				session.Send (message);
			}
		}
	}
}

