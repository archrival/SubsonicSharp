using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
	[XmlType(IncludeInSchema = false)]
	public enum ItemChoiceType
	{
		[XmlEnum("album")] Album,
		[XmlEnum("albumInfo")] AlbumInfo,
		[XmlEnum("albumInfo2")] AlbumInfo2,
		[XmlEnum("albumList")] AlbumList,
		[XmlEnum("albumList2")] AlbumList2,
		[XmlEnum("artist")] Artist,
		[XmlEnum("artistInfo")] ArtistInfo,
		[XmlEnum("artistInfo2")] ArtistInfo2,
		[XmlEnum("artists")] Artists,
		[XmlEnum("bookmarks")] Bookmarks,
		[XmlEnum("chatMessages")] ChatMessages,
		[XmlEnum("directory")] Directory,
		[XmlEnum("error")] Error,
		[XmlEnum("genres")] Genres,
		[XmlEnum("indexes")] Indexes,
		[XmlEnum("internetRadioStations")] InternetRadioStations,
		[XmlEnum("jukeboxPlaylist")] JukeboxPlaylist,
		[XmlEnum("jukeboxStatus")] JukeboxStatus,
		[XmlEnum("license")] License,
		[XmlEnum("lyrics")] Lyrics,
		[XmlEnum("musicFolders")] MusicFolders,
		[XmlEnum("nowPlaying")] NowPlaying,
		[XmlEnum("playlist")] Playlist,
		[XmlEnum("playlists")] Playlists,
		[XmlEnum("playQueue")] PlayQueue,
		[XmlEnum("podcasts")] Podcasts,
		[XmlEnum("randomSongs")] RandomSongs,
		[XmlEnum("searchResult")] SearchResult,
		[XmlEnum("searchResult2")] SearchResult2,
		[XmlEnum("searchResult3")] SearchResult3,
		[XmlEnum("shares")] Shares,
		[XmlEnum("similarSongs")] SimilarSongs,
		[XmlEnum("similarSongs2")] SimilarSongs2,
		[XmlEnum("song")] Song,
		[XmlEnum("songsByGenre")] Songs,
		[XmlEnum("starred")] Starred,
		[XmlEnum("starred2")] Starred2,
		[XmlEnum("topSongs")] TopSongs,
		[XmlEnum("user")] User,
		[XmlEnum("users")] Users,
		[XmlEnum("videos")] Videos
	}
}