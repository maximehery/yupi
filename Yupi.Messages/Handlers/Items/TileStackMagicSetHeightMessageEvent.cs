﻿using System;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Enums;

namespace Yupi.Messages.Items
{
	public class TileStackMagicSetHeightMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

			if (room == null)
				return;

			uint itemId = request.GetUInt32();

			RoomItem item = room.GetRoomItemHandler().GetItem(itemId);

			if (item == null || item.GetBaseItem().InteractionType != Interaction.TileStackMagic)
				return;

			int heightToSet = request.GetInteger();
			double totalZ;

			if (heightToSet < 0)
			{
				totalZ = room.GetGameMap().SqAbsoluteHeight(item.X, item.Y);

				router.GetComposer<UpdateTileStackMagicHeight> ().Compose (item.Id, totalZ);
			}
			else
			{
				if (heightToSet > 10000)
					heightToSet = 10000;

				totalZ = heightToSet/100.0;

				if (totalZ < room.RoomData.Model.SqFloorHeight[item.X][item.Y])
				{
					totalZ = room.RoomData.Model.SqFloorHeight[item.X][item.Y];
					router.GetComposer<UpdateTileStackMagicHeight> ().Compose (item.Id, totalZ);
				}
			}

			room.GetRoomItemHandler().SetFloorItem(item, item.X, item.Y, totalZ, item.Rot, true);
		}
	}
}
