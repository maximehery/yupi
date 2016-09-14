﻿using System;
using Yupi.Messages.Contracts;
using Yupi.Model.Domain;

namespace Yupi.Controller
{
    public class ChatController
    {
        public const int MAX_MESSAGE_LENGTH = 100;

        private bool Validate(ref string message)
        {
            if (message.Length > MAX_MESSAGE_LENGTH) return false;

            /* TODO Implement
                if (!ServerSecurityChatFilter.CanTalk(session, msg))
                    return false;
                    */

            // TODO Wordfilter
            // TODO Flood
            // TODO Room Mute

            return true;
        }

        private bool TryHandleCommand(string message)
        {
            /* TODO Command manager
                 * return msg.StartsWith(":") && CommandsManager.TryExecute(msg.Substring(1), session)
                */
            return false;
        }

        public void Whisper(Habbo session, string message, ChatBubbleStyle bubble, RoomEntity target, int count = -1)
        {
            if (!Validate(ref message) || TryHandleCommand(message) || !bubble.CanUse(session.Info)) return;

            session.RoomEntity.Wake();

            var entry = CreateMessage(session, message, bubble);
            entry.Whisper = true;
            // TODO Save Whisper target
            // TODO Save Chatlog

            session.Router.GetComposer<WhisperMessageComposer>().Compose(session, entry, count);

            if (target != null)
                target.HandleChatMessage(session.RoomEntity, targetSession =>
                {
                    targetSession.Router.GetComposer<WhisperMessageComposer>()
                        .Compose(targetSession, entry, count);
                });

            // TODO Trigger Wired
        }

        public void Shout(Habbo session, string message, ChatBubbleStyle bubble, int count = -1)
        {
            Chat(session, message, bubble, (user, entry) =>
            {
                user.Router.GetComposer<ShoutMessageComposer>()
                    .Compose(user, entry, count);
            });
        }

        public void Chat(Habbo session, string message, ChatBubbleStyle bubble, int count = -1)
        {
            Chat(session, message, bubble, (user, entry) =>
            {
                user.Router.GetComposer<ChatMessageComposer>()
                    .Compose(user, entry, count);
            });
        }

        private ChatMessage CreateMessage(Habbo session, string message, ChatBubbleStyle bubble)
        {
            var msg = new ChatMessage(message)
            {
                Entity = session.RoomEntity,
                Bubble = bubble,
                User = session.Info
            };

            session.Room.Data.Chatlog.Add(msg);
            return msg;
        }

        private void Chat(Habbo session, string message, ChatBubbleStyle bubble, Action<Habbo, ChatMessage> composer)
        {
            if (!Validate(ref message) || TryHandleCommand(message) || !bubble.CanUse(session.Info)) return;

            session.RoomEntity.Wake();

            var entry = CreateMessage(session, message, bubble);

            session.Info.Preferences.ChatBubbleStyle = bubble;
            // TODO Save Preferences
            // TODO Save Chatlog
            session.Room.EachEntity(
                entity => { entity.HandleChatMessage(session.RoomEntity, user => composer(user, entry)); }
            );

            // TODO Trigger Wired
        }
    }
}