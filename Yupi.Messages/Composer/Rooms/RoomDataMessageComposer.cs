﻿using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class RoomDataMessageComposer : AbstractComposer<RoomData, bool, bool>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, RoomData room, bool show, bool isNotReload)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(show);
				message.AppendBool(isNotReload);
				message.AppendBool(Yupi.GetGame().GetNavigator() != null && Yupi.GetGame().GetNavigator().GetPublicRoom(room.RoomData.Id) != null);
				message.AppendBool(!isNotReload || session.GetHabbo().HasFuse("fuse_mod"));
				message.AppendBool(room.IsMuted);
				message.AppendInteger(room.WhoCanMute);
				message.AppendInteger(room.WhoCanKick);
				message.AppendInteger(room.WhoCanBan);
				message.AppendBool(room.CheckRights(session, true));
				message.AppendInteger(room.ChatType);
				message.AppendInteger(room.ChatBalloon);
				message.AppendInteger(room.ChatSpeed);
				message.AppendInteger(room.ChatMaxDistance);
				message.AppendInteger(room.ChatFloodProtection);
				session.Send (message);
			}
		}
	}
}

