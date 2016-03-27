using System;
using Subsonic.Client.Extensions;
using Subsonic.Common.Classes;
using Subsonic.Client.Tasks;

namespace Subsonic.Client.Models
{
    public class ChatModel : ObservableObject
    {
        public string User { get; }
        public string Message { get; }
        public DateTime TimeStamp { get; }

        public ChatModel(ChatMessage chatMessage)
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
            hash = (hash * HashFactor) + typeof(ChatModel).GetHashCode();
            hash = (hash * HashFactor) + TimeStamp.GetHashCode();
            hash = (hash * HashFactor) + User.GetHashCode();
            hash = (hash * HashFactor) + Message.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as ChatModel);
        }

        private bool Equals(ChatModel chatItem)
        {
            return chatItem != null && this == chatItem;
        }

        public static bool operator ==(ChatModel left, ChatModel right)
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

        public static bool operator !=(ChatModel left, ChatModel right)
        {
            return !(left == right);
        }
        #endregion
    }
}