﻿using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
    public class TradeAcceptMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CanTradeInRoom)
                return;

            Trade userTrade = room.GetUserTrade(session.GetHabbo().Id);

            userTrade?.Accept(session.GetHabbo().Id);
            */
            throw new NotImplementedException();
        }
    }
}