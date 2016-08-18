﻿using System.Collections.Generic;
using System;

namespace Yupi.Model.Domain
{
	public class TargetedOffer : CatalogItem
    {
		public virtual DateTime ExpiresAt { get; set; }
		public virtual int PurchaseLimit { get; set; }
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual string Image { get; set; }
		public virtual string Icon { get; set; }

		public virtual IList<BaseItem> Products { get; protected set; }

		public TargetedOffer ()
		{
			Products = new List<BaseItem> ();
			Icon = string.Empty;
		}

		public override bool CanPurchase (Yupi.Model.Domain.Components.UserWallet wallet, int amount = 1)
		{
			// TODO Implement PurchaseLimit
			return base.CanPurchase (wallet, amount);
		}
    }
}