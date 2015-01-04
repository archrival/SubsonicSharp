using System;
using Subsonic.Client.Extensions;
using Subsonic.Common.Classes;

namespace Subsonic.Client.Items
{
    public class ChatItem
    {
        public string User { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }

        public ChatItem(ChatMessage chatMessage)
        {
            User = chatMessage.Username;
            Message = chatMessage.Message;
            TimeStamp = chatMessage.Time.FromUnixTimestampInMilliseconds().ToLocalTime();
        }

        // Overrides for equality
        #region HashCode and Equality Overrides
        private const int HashSeed = 29; // Should be prime number
        private const int HashFactor = 11; // Should be prime number

        public override int GetHashCode()
        {
            int hash = HashSeed;
            hash = (hash * HashFactor) + typeof(ChatItem).GetHashCode();
            hash = (hash * HashFactor) + TimeStamp.GetHashCode();
            hash = (hash * HashFactor) + User.GetHashCode();
            hash = (hash * HashFactor) + Message.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as ChatItem);
        }

        private bool Equals(ChatItem chatItem)
        {
            return chatItem != null && this == chatItem;
        }

        public static bool operator ==(ChatItem left, ChatItem right)
        {
            if (ReferenceEquals(null, left))
                return ReferenceEquals(null, right);

            if (!ReferenceEquals(null, right))
                if (left.TimeStamp.Equals(right.TimeStamp))
                    if (left.User.Equals(right.User))
                        if (left.Message.Equals(right.Message))
                            return true;

            return false;
        }

        public static bool operator !=(ChatItem left, ChatItem right)
        {
            return !(left == right);
        }
        #endregion
    }
}