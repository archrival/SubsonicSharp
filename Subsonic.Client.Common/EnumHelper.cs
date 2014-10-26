using System;

namespace Subsonic.Client
{
    public static class EnumHelper
    {
        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [XmlEnum("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetXmlEnumAttribute(this Enum en)
        {
            if (en == null) throw new ArgumentNullException("en");

            Type type = en.GetType();

            var memInfo = type.GetMember(en.ToString());

            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(System.Xml.Serialization.XmlEnumAttribute), false);

                if (attrs.Length > 0)
                    return ((System.Xml.Serialization.XmlEnumAttribute)attrs[0]).Name;
            }

            return en.ToString();
        }

    }
}
