using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Subsonic.Client
{
    public static class XmlExtensions
    {
        /// <summary>
        /// Deserialize XML string into object type specified.
        /// </summary>
        /// <typeparam name="T">Object type to deserialize the XML into.</typeparam>
        /// <param name="xml">XML string to deserialize.</param>
        /// <param name="ignoreNamespace"></param>
        /// <returns>T</returns>
        public static T DeserializeFromXml<T>(this string xml, bool ignoreNamespace = true)
        {
            T result;

            if (ignoreNamespace)
                xml = Regex.Replace(xml, @"(xmlns:?[^=]*=[""][^""]*[""])", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            var xmlSerializer = new XmlSerializer(typeof (T));

            using (var sr = new StringReader(xml))
                result = (T) xmlSerializer.Deserialize(sr);

            return result;
        }
    }
}