﻿using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RoomAlterFilterMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var roomId = request.GetUInt32();
            var shouldAdd = request.GetBool();
            var word = request.GetString();

            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true))
                return;

            if (!shouldAdd) {
                if (!room.WordFilter.Contains (word))
                    return;

                room.WordFilter.Remove (word);

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                    queryReactor.SetQuery ("DELETE FROM rooms_wordfilter WHERE room_id = @id AND word = @word");
                    queryReactor.AddParameter ("id", roomId);
                    queryReactor.AddParameter ("word", word);
                    queryReactor.RunQuery ();
                }
            } else {

                if (room.WordFilter.Contains (word))
                    return;

                if (word.Contains ("+")) {
                    session.SendNotif (Yupi.GetLanguage ().GetVar ("character_error_plus"));
                    return;
                }

                room.WordFilter.Add (word);

                using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                    queryreactor2.SetQuery ("INSERT INTO rooms_wordfilter (room_id, word) VALUES (@id, @word);");
                    queryreactor2.AddParameter ("id", roomId);
                    queryreactor2.AddParameter ("word", word);
                    queryreactor2.RunQuery ();
                }
            }*/
            throw new NotImplementedException();
        }
    }
}