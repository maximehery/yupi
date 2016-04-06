using System;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Emulator.Game.Items.Handlers
{
    /// <summary>
    ///     Class HopperHandler.
    /// </summary>
     public static class HopperHandler
    {
        /// <summary>
        ///     Gets a hopper.
        /// </summary>
        /// <param name="curRoom">The current room.</param>
        /// <returns>System.UInt32.</returns>
     public static uint GetAHopper(uint curRoom)
        {
            uint result;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    $"SELECT room_id FROM items_hopper WHERE room_id <> '{curRoom}' ORDER BY RAND() LIMIT 1");

                uint num = Convert.ToUInt32(queryReactor.GetInteger());
                result = num;
            }

            return result;
        }

        /// <summary>
        ///     Gets the hopper identifier.
        /// </summary>
        /// <param name="nextRoom">The next room.</param>
        /// <returns>System.UInt32.</returns>
     public static uint GetHopperId(uint nextRoom)
        {
            uint result;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT hopper_id FROM items_hopper WHERE room_id = @room LIMIT 1");
                queryReactor.AddParameter("room", nextRoom);

                string theString = queryReactor.GetString();

                result = theString == null ? 0u : Convert.ToUInt32(theString);
            }

            return result;
        }
    }
}