﻿using System;
using Yupi.Emulator.Game.Catalogs.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
	public class TargetedOfferMessageComposer : AbstractComposer<TargetedOffer>
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, TargetedOffer offer)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				message.AppendInteger(offer.Id);
				message.AppendString(offer.Identifier);
				message.AppendString(offer.Identifier);
				message.AppendInteger(offer.CostCredits);

				if (offer.CostDiamonds > 0)
				{
					message.AppendInteger(offer.CostDiamonds);
					message.AppendInteger(105); // TODO Hardcoded
				}
				else
				{
					message.AppendInteger(offer.CostDuckets);
					message.AppendInteger(0);
				}

				message.AppendInteger(offer.PurchaseLimit);

				int timeLeft = offer.ExpirationTime - Yupi.GetUnixTimeStamp();

				message.AppendInteger(timeLeft);
				message.AppendString(offer.Title);
				message.AppendString(offer.Description);
				message.AppendString(offer.Image);
				message.AppendString(string.Empty);
				message.StartArray();

				foreach (string product in offer.Products)
				{
					message.AppendString(product);
					message.SaveArray();
				}

				message.EndArray();

				session.Send (message);
			}
		}
	}
}

