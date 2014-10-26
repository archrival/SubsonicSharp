using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class Users
    {
        [XmlElement("user")]
        public List<User> User;
    }
}
