﻿using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class HeightMapMessageComposer : AbstractComposer<Gamemap>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Gamemap map)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(map.Model.MapSizeX);
				message.AppendInteger(map.Model.MapSizeX*map.Model.MapSizeY);
				for (int i = 0; i < map.Model.MapSizeY; i++)
				{
					for (int j = 0; j < map.Model.MapSizeX; j++)
					{
						message.AppendShort((short) (map.SqAbsoluteHeight(j, i)*256));
					}
				}
				session.Send (message);
			}
		}
	}
}

