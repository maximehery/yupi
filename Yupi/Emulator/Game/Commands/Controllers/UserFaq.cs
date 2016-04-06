﻿using System.Data;
using System.Text;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class FastWalk. This class cannot be inherited.
    /// </summary>
     public sealed class UserFaq : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FastWalk" /> class.
        /// </summary>
        public UserFaq()
        {
            MinRank = 0;
            Description = "FAQ";
            Usage = ":faq";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room targetRoom = session.GetHabbo().CurrentRoom;
            DataTable data;
            using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("SELECT question, answer FROM rooms_faq");
                data = dbClient.GetTable();
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(" - FAQ - \r\r");

            foreach (DataRow row in data.Rows)
            {
                builder.Append("Q: " + (string) row["question"] + "\r");
                builder.Append("A: " + (string) row["answer"] + "\r\r");
            }
            session.SendNotif(builder.ToString());
            return true;
        }
    }
}