using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Subsonic.Client.Common
{
    public static class EnumHelper
    {
        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetXmlEnumAttribute(Enum en)
        {
            Type type = en.GetType();

            var memInfo = type.GetMember(en.ToString());

            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(System.Xml.Serialization.XmlEnumAttribute), false);

                if (attrs.Length > 0)
                    return ((System.Xml.Serialization.XmlEnumAttribute) attrs[0]).Name;
            }

            return en.ToString();
        }

    }
}
