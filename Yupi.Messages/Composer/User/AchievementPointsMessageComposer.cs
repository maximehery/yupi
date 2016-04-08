﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class AchievementPointsMessageComposer : AbstractComposer<int>
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, int points)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (points);
				session.Send (message);
			}
		}
	}
}

