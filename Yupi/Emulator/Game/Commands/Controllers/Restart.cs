﻿using System.Threading.Tasks;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Shutdown. This class cannot be inherited.
    /// </summary>
     public sealed class Restart : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Shutdown" /> class.
        /// </summary>
        public Restart()
        {
            MinRank = 10;
            Description = "Restart the Server.";
            Usage = ":restart";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Yupi.GetGame()
                .GetModerationTool()
                .LogStaffEntry(session.GetHabbo().UserName, string.Empty, "Restart",
                    "Issued Restart command!");
            new Task(Yupi.PerformRestart).Start();
            return true;
        }
    }
}