﻿using System;





namespace Yupi.Messages.User
{
	public class UserUpdateMottoMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string motto = message.GetString ();

			Habbo habbo = session.GetHabbo ();

			if (motto == habbo.Motto)
				return;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery("UPDATE users SET motto = @motto WHERE id = @user");
				queryReactor.AddParameter("motto", text);
				queryReactor.AddParameter("user", habbo.Id);
				queryReactor.RunQuery();
			}

			Yupi.Messages.Rooms currentRoom = session.GetHabbo().CurrentRoom;
			RoomUser roomUserByHabbo = currentRoom?.GetRoomUserManager().GetRoomUserByHabbo(habbo.Id);

			if (roomUserByHabbo == null)
				return;

			router.GetComposer<UpdateUserDataMessageComposer> ().Compose (currentRoom, habbo, roomUserByHabbo.VirtualId);

			Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_Motto", 1);
		}
	}
}

