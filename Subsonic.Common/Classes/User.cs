using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class User
    {
        [XmlElement("folder")] public List<int> Folder;
        [XmlAttribute("adminRole")] public bool AdminRole;
        [XmlAttribute("commentRole")] public bool CommentRole;
        [XmlAttribute("coverArtRole")] public bool CoverArtRole;
        [XmlAttribute("downloadRole")] public bool DownloadRole;
        [XmlAttribute("email")] public string Email;
        [XmlAttribute("jukeboxRole")] public bool JukeboxRole;
        [XmlAttribute("maxBitrate")] public int MaxBitrate;
        [XmlAttribute("playlistRole")] public bool PlaylistRole;
        [XmlAttribute("podcastRole")] public bool PodcastRole;
        [XmlAttribute("scrobblingEnabled")] public bool ScrobblingEnabled;
        [XmlAttribute("settingsRole")] public bool SettingsRole;
        [XmlAttribute("shareRole")] public bool ShareRole;
        [XmlAttribute("streamRole")] public bool StreamRole;
        [XmlAttribute("uploadRole")] public bool UploadRole;
        [XmlAttribute("username")] public string Username;
        [XmlAttribute("videoConversionRole")] public bool VideoConversionRole;
        [XmlAttribute("avatarLastChanged")] public DateTime AvatarLastChanged;
    }
}
