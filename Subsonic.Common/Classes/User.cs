using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
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

        [XmlElement("folder")]
        public List<int> Folder;

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

        [XmlAttribute("videoConversionRole")]
        public bool VideoConversionRole;

        [XmlIgnore]
        private DateTime? _avatarLastChanged;

        [XmlIgnore]
        private int? _maxBitrate;

        [XmlAttribute("avatarLastChanged")]
        public DateTime AvatarLastChanged
        {
            get => _avatarLastChanged.GetValueOrDefault();
            set => _avatarLastChanged = value;
        }

        [XmlAttribute("maxBitrate")]
        public int MaxBitrate
        {
            get => _maxBitrate.GetValueOrDefault();
            set => _maxBitrate = value;
        }

        public bool ShouldSerializeAvatarLastChanged()
        {
            return _avatarLastChanged.HasValue;
        }

        public bool ShouldSerializeMaxBitrate()
        {
            return _maxBitrate.HasValue;
        }
    }
}