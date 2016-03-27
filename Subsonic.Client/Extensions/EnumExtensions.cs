using System;
using System.Linq;
using System.Reflection;

namespace Subsonic.Client.Extensions
{
    public static class EnumExtensions
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
            if (en == null) throw new ArgumentNullException(nameof(en));

            Type type = en.GetType();

            var memInfo = type.GetTypeInfo().DeclaredMembers.Where(m => m.Name == en.ToString()).ToList();

            if (!memInfo.Any())
                return en.ToString();

            var attrs = memInfo.First().CustomAttributes.Where(ca => ca.AttributeType == typeof(System.Xml.Serialization.XmlEnumAttribute)).ToList();

            if (attrs.Any())
                return attrs.First().ConstructorArguments.First().Value as string;

            return en.ToString();
        }
    }
}
