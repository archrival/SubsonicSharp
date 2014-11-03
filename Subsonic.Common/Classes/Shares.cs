using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Shares
    {
        [XmlElement("share")] public List<Share> Share;
    }
}
