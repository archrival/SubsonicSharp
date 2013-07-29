using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class ChatMessages
    {
        [XmlElement("chatMessage")]
        public List<ChatMessage> ChatMessage;
    }
}
