using Subsonic.Client.Models;

namespace Subsonic.Client.EventArgs
{
    public class ChatEventArgs : System.EventArgs
    {
        public ChatModel ChatItem { get; set; }

        public ChatEventArgs(ChatModel chatItem)
        {
            ChatItem = chatItem;
        }
    }
}