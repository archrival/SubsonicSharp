using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class ScanStatus
    {
        [XmlIgnore]
        public long? _count;

        [XmlAttribute("scanning")]
        public bool Scanning;

        [XmlAttribute("count")]
        public long Count
        {
            get => _count.GetValueOrDefault();
            set => _count = value;
        }

        public bool ShouldSerializeCount()
        {
            return _count.HasValue;
        }
    }
}