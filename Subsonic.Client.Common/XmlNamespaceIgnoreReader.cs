using System.IO;
using System.Xml;

namespace Subsonic.Client.Common
{
    public class XmlNamespaceIgnoreReader : XmlTextReader
    {
        private readonly string _newNamespaceUri;

        public XmlNamespaceIgnoreReader(TextReader reader, string newNamespaceUri) : base(reader)
        {
            _newNamespaceUri = newNamespaceUri;
        }

        public override string NamespaceURI
        {
            get
            {
                return NodeType != XmlNodeType.Attribute ? _newNamespaceUri : base.NamespaceURI;
            }
        }
    }
}
