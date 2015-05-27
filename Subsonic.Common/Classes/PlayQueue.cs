using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
	public class PlayQueue
	{
		[XmlElement("entry")] public List<Child> Entries;
		[XmlAttribute("current")] public string Current;
		[XmlAttribute("position")] public long Position;
		[XmlAttribute("username")] public string Username;
		[XmlAttribute("changed")] public DateTime Changed;
		[XmlAttribute("changedBy")] public string ChangedBy;
	}
}
