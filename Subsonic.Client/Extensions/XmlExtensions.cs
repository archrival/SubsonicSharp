using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Subsonic.Client.Extensions
{
    public static class XmlExtensions
    {
        /// <summary>
        /// Deserialize XML string into object type specified.
        /// </summary>
        /// <typeparam name="T">Object type to deserialize the XML into.</typeparam>
        /// <param name="xml">XML string to deserialize.</param>
        /// <param name="ignoreNamespace"></param>
        /// <returns>Deserialized object T</returns>
        public static T DeserializeFromXml<T>(this string xml, bool ignoreNamespace = true)
        {
            T result;

            if (ignoreNamespace)
                xml = Regex.Replace(xml, @"(xmlns:?[^=]*=[""][^""]*[""])", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(xml))
                result = (T)xmlSerializer.Deserialize(sr);

            return result;
        }

        /// <summary>
        /// Deserialize XML string into object type specified.
        /// </summary>
        /// <typeparam name="T">Object type to deserialize the XML into.</typeparam>
        /// <param name="xml">XML string to deserialize.</param>
        /// <param name="ignoreNamespace"></param>
        /// <returns>Deserialized object T</returns>
        public static async Task<T> DeserializeFromXmlAsync<T>(this string xml, bool ignoreNamespace = true)
        {
            return await Task.FromResult(xml.DeserializeFromXml<T>(ignoreNamespace));
        }
    }
}