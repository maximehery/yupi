﻿using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Messages.Guides
{
	public class GuideInviteToRoom : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			GameClient requester = session.GetHabbo().GuideOtherUser;

			Room room = session.GetHabbo().CurrentRoom;

			if (room == null) {
				return;
			}

			router.GetComposer<OnGuideSessionInvitedToGuideRoomMessageComposer> ().Compose (requester, room.RoomId, room.RoomData.Name);

			// TODO Is this really required
			router.GetComposer<OnGuideSessionInvitedToGuideRoomMessageComposer> ().Compose (session, room.RoomId, room.RoomData.Name);
		}
	}
}

