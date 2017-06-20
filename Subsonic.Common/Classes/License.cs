using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class License
    {
        [XmlAttribute("email")]
        public string Email;

        [XmlAttribute("valid")]
        public bool Valid;

        [XmlIgnore]
        private DateTime? _licenseExpires;

        [XmlIgnore]
        private DateTime? _trialExpires;

        [XmlAttribute("licenseExpires")]
        public DateTime LicenseExpires
        {
            get { return _licenseExpires.GetValueOrDefault(); }
            set { _licenseExpires = value; }
        }

        [XmlAttribute("trialExpires")]
        public DateTime TrialExpires
        {
            get { return _trialExpires.GetValueOrDefault(); }
            set { _trialExpires = value; }
        }

        public bool ShouldSerializeLicenseExpires()
        {
            return _licenseExpires.HasValue;
        }

        public bool ShouldSerializeTrialExpires()
        {
            return _trialExpires.HasValue;
        }
    }
}