using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class Shares
    {
        [XmlElement("share")]
        public List<Share> Share;
    }
}
