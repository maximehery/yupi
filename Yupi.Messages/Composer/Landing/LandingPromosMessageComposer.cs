﻿using System;
using System.Collections.Generic;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Landing
{
	public class LandingPromosMessageComposer : AbstractComposer<List<HotelLandingPromos>>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, List<HotelLandingPromos> promos)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(promos.Count);

				foreach (HotelLandingPromos promo in promos) {
					message.AppendInteger (promo.Index);
					message.AppendString (promo.Header);
					message.AppendString (promo.Body);
					message.AppendString (promo.Button);
					message.AppendInteger (promo.InGamePromo);
					message.AppendString (promo.SpecialAction);
					message.AppendString (promo.Image);
				}
				
				session.Send (message);
			}
		}
	}
}

