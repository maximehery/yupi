using System;

namespace Yupi.Model.Domain
{
    public class MessengerMessage
    {
        public virtual int Id { get; protected set; }
        public virtual UserInfo From { get; set; }
        public virtual UserInfo To { get; set; }
        public virtual string Text { get; set; }
        public virtual string UnfilteredText { get; set; }
        public virtual DateTime Timestamp { get; set; }
        public virtual bool Read { get; set; }

        public virtual TimeSpan Diff()
        {
            return DateTime.Now - Timestamp;
        }
    }
}