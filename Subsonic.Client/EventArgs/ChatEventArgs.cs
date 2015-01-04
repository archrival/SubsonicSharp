using Subsonic.Client.Items;

namespace Subsonic.Client.EventArgs
{
    public class ChatEventArgs : System.EventArgs
    {
        public ChatItem ChatItem { get; set; }

        public ChatEventArgs(ChatItem chatItem)
        {
            ChatItem = chatItem;
        }
    }
}
