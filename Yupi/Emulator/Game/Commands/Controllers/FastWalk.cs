﻿using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class FastWalk. This class cannot be inherited.
    /// </summary>
     public sealed class FastWalk : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FastWalk" /> class.
        /// </summary>
        public FastWalk()
        {
            MinRank = -3;
            Description = "Enable/Disable Fast Walk.";
            Usage = ":fastwalk";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            RoomUser user =
                Yupi.GetGame()
                    .GetRoomManager()
                    .GetRoom(session.GetHabbo().CurrentRoomId)
                    .GetRoomUserManager()
                    .GetRoomUserByHabbo(session.GetHabbo().Id);
            user.FastWalking = !user.FastWalking;
            return true;
        }
    }
}