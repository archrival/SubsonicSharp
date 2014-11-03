using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Users
    {
        [XmlElement("user")] public List<User> User;
    }
}
