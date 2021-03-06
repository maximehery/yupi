﻿// ---------------------------------------------------------------------------------
// <copyright file="ModerationToolPerformRoomActionMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Messages.Support
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Controller;
    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Util;

    public class ModerationToolPerformRoomActionMessageEvent : AbstractHandler
    {
        #region Fields

        private RoomManager RoomManager;
        private IRepository<RoomData> RoomRepository;

        #endregion Fields

        #region Constructors

        public ModerationToolPerformRoomActionMessageEvent()
        {
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (!session.Info.HasPermission("fuse_mod"))
                return;

            int roomId = message.GetInteger();

            // TODO Refactor (shoud be enum)
            bool lockRoom = message.GetIntegerAsBool();
            bool inappropriateRoom = message.GetIntegerAsBool();
            bool kickUsers = message.GetIntegerAsBool();

            RoomData roomData = RoomRepository.Find(roomId);

            if (roomData == null)
            {
                return;
            }

            if (lockRoom)
            {
                roomData.State = RoomState.Locked;
            }

            Room room = null;

            if (inappropriateRoom || kickUsers)
            {
                room = RoomManager.LoadedRooms.FirstOrDefault(x => x.Data.Id == roomData.Id);
            }

            if (inappropriateRoom)
            {
                // TODO Translate
                roomData.Name = T._("Inappropriate for Hotel Management");
                roomData.Description = string.Empty;
                roomData.Tags.Clear();

                if (room != null)
                {
                    room.EachUser(
                        (entitySession) =>
                        {
                            entitySession.Router.GetComposer<RoomDataMessageComposer>()
                                .Compose(entitySession, roomData, entitySession.Info, false, true);
                        });
                }
            }

            if (kickUsers && room != null)
            {
                RoomManager.KickAll(room);
            }
        }

        #endregion Methods
    }
}