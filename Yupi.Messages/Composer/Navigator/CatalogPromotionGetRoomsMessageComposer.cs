﻿using System;
using Yupi.Emulator.Game.Rooms.Data.Models;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
	public class CatalogPromotionGetRoomsMessageComposer : AbstractComposer<HashSet<RoomData>>
	{
		public override void Compose (Yupi.Protocol.ISender session, HashSet<RoomData> rooms)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(true);
				message.AppendInteger(rooms.Count);

				foreach (RoomData room in rooms)
				{
					message.AppendInteger(room.Id);
					message.AppendString(room.Name);
					message.AppendBool(false);
				}
				session.Send (message);
			}
		}
	}
}
