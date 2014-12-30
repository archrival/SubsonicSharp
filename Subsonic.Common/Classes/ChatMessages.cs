using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class ChatMessages
    {
        [XmlElement("chatMessage")] public List<ChatMessage> Items;
    }
}
