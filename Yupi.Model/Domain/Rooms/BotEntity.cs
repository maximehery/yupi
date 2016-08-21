﻿using System;
using Yupi.Model.Domain;

namespace Yupi.Model.Domain
{
	[Ignore]
	public class BotEntity : RoomEntity
	{
		public BotInfo Info { get; set; }

		public BotEntity (Room room) : base(room)
		{
			
		}

		public override EntityType Type {
			get {
				return EntityType.Bot;
			}
		}
	}
}

