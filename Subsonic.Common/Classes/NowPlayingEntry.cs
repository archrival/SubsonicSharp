using System;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class NowPlayingEntry : Child
    {
        [XmlAttribute("minutesAgo")]
        public int MinutesAgo;

        [XmlAttribute("playerId")]
        public int PlayerId;

        [XmlAttribute("playerName")]
        public string PlayerName;

        [XmlAttribute("username")]
        public string Username;
    }
}
