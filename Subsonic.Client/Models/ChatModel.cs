using Subsonic.Client.Extensions;
using Subsonic.Client.Tasks;
using Subsonic.Common.Classes;
using System;

namespace Subsonic.Client.Models
{
    public class ChatModel : ObservableObject
    {
        public ChatModel(ChatMessage chatMessage)
        {
            User = chatMessage.Username;
            Message = chatMessage.Message;
            TimeStamp = chatMessage.Time.FromUnixTimestampInMilliseconds().ToLocalTime();
        }

        public string Message { get; }
        public DateTime TimeStamp { get; }
        public string User { get; }
        // Overrides for equality

        #region HashCode and Equality Overrides

        private const int HashFactor = 11;
        private const int HashSeed = 29; // Should be prime number
                                         // Should be prime number

        public static bool operator !=(ChatModel left, ChatModel right)
        {
            return !(left == right);
        }

        public static bool operator ==(ChatModel left, ChatModel right)
        {
            if (left is null)
                return right is null;

            if (!(right is null))
                if (left.TimeStamp.Equals(right.TimeStamp))
                    if (left.User.Equals(right.User))
                        if (left.Message.Equals(right.Message))
                            return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as ChatModel);
        }

        public override int GetHashCode()
        {
            var hash = HashSeed;
            hash = hash * HashFactor + typeof(ChatModel).GetHashCode();
            hash = hash * HashFactor + TimeStamp.GetHashCode();
            hash = hash * HashFactor + User.GetHashCode();
            hash = hash * HashFactor + Message.GetHashCode();

            return hash;
        }

        private bool Equals(ChatModel chatItem)
        {
            return chatItem != null && this == chatItem;
        }

        #endregion HashCode and Equality Overrides
    }
}