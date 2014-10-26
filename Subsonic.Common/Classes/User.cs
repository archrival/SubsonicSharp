using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class User
    {
        [XmlAttribute("adminRole")]
        public bool AdminRole;

        [XmlAttribute("commentRole")]
        public bool CommentRole;

        [XmlAttribute("coverArtRole")]
        public bool CoverArtRole;

        [XmlAttribute("downloadRole")]
        public bool DownloadRole;

        [XmlAttribute("email")]
        public string Email;

        [XmlAttribute("jukeboxRole")]
        public bool JukeboxRole;

        [XmlAttribute("playlistRole")]
        public bool PlaylistRole;

        [XmlAttribute("podcastRole")]
        public bool PodcastRole;

        [XmlAttribute("scrobblingEnabled")]
        public bool ScrobblingEnabled;

        [XmlAttribute("settingsRole")]
        public bool SettingsRole;

        [XmlAttribute("shareRole")]
        public bool ShareRole;

        [XmlAttribute("streamRole")]
        public bool StreamRole;

        [XmlAttribute("uploadRole")]
        public bool UploadRole;

        [XmlAttribute("username")]
        public string Username;
    }
}
