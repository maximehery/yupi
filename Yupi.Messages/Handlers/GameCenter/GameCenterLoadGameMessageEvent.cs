﻿using System;

namespace Yupi.Messages.GameCenter
{
	public class GameCenterLoadGameMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<GameCenterGameAchievementsMessageComposer> ().Compose (session);
			router.GetComposer<GameCenterLeaderboardMessageComposer> ().Compose (session);
			router.GetComposer<GameCenterLeaderboard2MessageComposer> ().Compose (session);
			router.GetComposer<GameCenterGamesLeftMessageComposer> ().Compose (session);
			router.GetComposer<GameCenterPreviousWinnerMessageComposer> ().Compose (session);
			router.GetComposer<GameCenterAllAchievementsMessageComposer> ().Compose (session);
			router.GetComposer<GameCenterEnterInGameMessageComposer> ().Compose (session);
		}
	}
}

