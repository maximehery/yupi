﻿using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Groups.Structs;
using System.Collections.Generic;

namespace Yupi.Messages.Groups
{
	public class GroupForumReadThreadMessageComposer : AbstractComposer
	{
		// TODO What is b good for?
		public void Compose(GameClient session, int groupId, int threadId, int startIndex, int b, List<GroupForumPost> posts) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(groupId);
				message.AppendInteger(threadId);
				message.AppendInteger(startIndex);
				message.AppendInteger(b);

				int indx = 0;

				foreach (GroupForumPost post in posts)
				{
					message.AppendInteger(indx);
					message.AppendInteger(indx);
					message.AppendInteger(post.PosterId);
					message.AppendString(post.PosterName);
					message.AppendString(post.PosterLook);
					message.AppendInteger(Yupi.GetUnixTimeStamp() - post.Timestamp);
					message.AppendString(post.PostContent);
					message.AppendByte(0);
					message.AppendInteger(0);
					message.AppendString(post.Hider);
					message.AppendInteger(0);
					message.AppendInteger(0);

					indx++;
				}
				session.Send (message);
			}
		}
	}
}
