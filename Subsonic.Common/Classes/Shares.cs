using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class Shares
    {
        [XmlElement("share")]
        public List<Share> Share;
    }
}
