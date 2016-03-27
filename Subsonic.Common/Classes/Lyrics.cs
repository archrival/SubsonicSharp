using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Lyrics
    {
        [XmlAttribute("artist")] public string Artist;
        [XmlText] public string Text;
        [XmlAttribute("title")] public string Title;
    }
}
