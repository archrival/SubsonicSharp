﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class NowPlaying
    {
        [XmlElement("entry")]
        public List<NowPlayingEntry> Entry;

    }
}
