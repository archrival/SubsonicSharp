using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class InternetRadioStations
    {
        [XmlElement("internetRadioStation")]
        public List<InternetRadioStation> Items;
    }
}