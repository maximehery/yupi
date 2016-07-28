﻿using System;
using System.Collections.Generic;

namespace Yupi.Messages.User
{
	public class HotelViewRequestBadgeMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string name = message.GetString();

			Dictionary<string, string> hotelViewBadges = Yupi.GetGame().GetHotelView().HotelViewBadges;

			if (!hotelViewBadges.ContainsKey(name))
				return;

			string badge = hotelViewBadges[name];
			session.GetHabbo().GetBadgeComponent().GiveBadge(badge, true, session, true);
		}
	}
}

