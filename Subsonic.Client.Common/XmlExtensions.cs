using System.IO;
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
        /// <returns>T</returns>
        public static T DeserializeFromXml<T>(this string xml)
        {
            T result;

            var xmlSerializer = new XmlSerializer(typeof (T));

            using (var sr = new StringReader(xml))
                result = (T) xmlSerializer.Deserialize(new XmlNamespaceIgnoreReader(sr, string.Empty));

            return result;
        }
    }
}