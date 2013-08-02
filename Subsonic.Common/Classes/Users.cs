using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class Users
    {
        [XmlElement("user")]
        public List<User> User;
    }
}
