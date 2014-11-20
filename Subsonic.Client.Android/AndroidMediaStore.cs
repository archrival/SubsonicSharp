using Android.Content;
using Android.Provider;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Subsonic.Client.Android
{
    public class AndroidMediaStore
    {
        Context _context { get; set; }

        public AndroidMediaStore(Context context)
        {
            _context = context;
        }

        public async Task<List<AndroidTrackItem>> GetTracksAsync(string selection = null, string[] selectionArgs = null, string sortOrder = null)
        {
            return await Task.Run(() =>
                {
                    var trackItems = new List<AndroidTrackItem>();

                    ContentResolver cr = _context.ContentResolver;
                    var uri = MediaStore.Audio.Media.ExternalContentUri;

                    const string id = MediaStore.Audio.Media.InterfaceConsts.Id; 
                    const string albumName = MediaStore.Audio.Media.InterfaceConsts.Album;
                    const string artist = MediaStore.Audio.Media.InterfaceConsts.Artist;
                    const string title = MediaStore.Audio.Media.InterfaceConsts.Title;
                    const string year = MediaStore.Audio.Media.InterfaceConsts.Year;
                    const string track = MediaStore.Audio.Media.InterfaceConsts.Track;

                    string[] columns = { id, albumName, artist, title, year, track };

                    using (var cursor = cr.Query(uri, columns, selection, selectionArgs, sortOrder))
                    {
                        int idColumn = cursor.GetColumnIndex(id);
                        int albumNameColumn = cursor.GetColumnIndex(albumName);
                        int artistColumn = cursor.GetColumnIndex(artist);
                        int yearColumn = cursor.GetColumnIndex(year);
                        int trackNumberColumn = cursor.GetColumnIndex(track);
                        int titleColumn = cursor.GetColumnIndex(title);

                        cursor.MoveToFirst();

                        while (!cursor.IsAfterLast)
                        {
                            string trackId = cursor.GetString(idColumn);
                            string trackAlbumName = cursor.GetString(albumNameColumn);
                            string artistName = cursor.GetString(artistColumn);
                            int trackYear = cursor.GetInt(yearColumn);
                            int trackNumber = cursor.GetInt(trackNumberColumn);
                            string titleName = cursor.GetString(titleColumn);

                            AndroidTrackItem trackItem = new AndroidTrackItem
                                {
                                    Id = trackId,
                                    Artist = artistName,
                                    Year = trackYear,
                                    Album = trackAlbumName,
                                    Title = titleName,
                                    TrackNumber = trackNumber,
                                };

                            trackItems.Add(trackItem);

                            cursor.MoveToNext();
                        }
                    }

                    return trackItems;
                });
        }

        public async Task<List<AndroidAlbumItem>> GetAlbumsAsync(string selection = null, string[] selectionArgs = null, string sortOrder = null)
        {
            return await Task.Run(() =>
                {
                    var albumItems = new List<AndroidAlbumItem>();

                    ContentResolver cr = _context.ContentResolver;
                    var uri = MediaStore.Audio.Albums.ExternalContentUri;

                    const string id = MediaStore.Audio.Albums.InterfaceConsts.Id;
                    const string albumName = MediaStore.Audio.Albums.InterfaceConsts.Album;
                    const string artist = MediaStore.Audio.Albums.InterfaceConsts.Artist;
                    const string firstYear = MediaStore.Audio.Albums.InterfaceConsts.FirstYear;
                    const string lastYear = MediaStore.Audio.Albums.InterfaceConsts.LastYear;
                    const string albumArt = MediaStore.Audio.Albums.InterfaceConsts.AlbumArt;

                    string[] columns = { id, albumName, artist, firstYear, lastYear, albumArt };

                    using (var cursor = cr.Query(uri, columns, selection, selectionArgs, sortOrder))
                    {
                        int idColumn = cursor.GetColumnIndex(id);
                        int albumNameColumn = cursor.GetColumnIndex(albumName);
                        int artistColumn = cursor.GetColumnIndex(artist);
                        int firstYearColumn = cursor.GetColumnIndex(firstYear);
                        int lastYearColumn = cursor.GetColumnIndex(lastYear);
                        int albumArtColumn = cursor.GetColumnIndex(albumArt);

                        cursor.MoveToFirst();

                        while (!cursor.IsAfterLast)
                        {
                            string albumAlbumId = cursor.GetString(idColumn);
                            string name = cursor.GetString(albumNameColumn);
                            string artistName = cursor.GetString(artistColumn);
                            int year = cursor.GetInt(firstYearColumn);
                            string coverArt = cursor.GetString(albumArtColumn);

                            AndroidAlbumItem albumItem = new AndroidAlbumItem
                                {
                                    Id = albumAlbumId,
                                    Artist = artistName,
                                    Year = year,
                                    Name = name,
                                    CoverArt = coverArt
                                };

                            albumItems.Add(albumItem);
                        
                            cursor.MoveToNext();
                        }
                    }

                    return albumItems;
                });
        }

        public async Task<List<AndroidArtistItem>> GetArtistsAsync(string selection = null, string[] selectionArgs = null, string sortOrder = null)
        {
            return await Task.Run(() =>
                {
                    var artistItems = new List<AndroidArtistItem>();

                    ContentResolver cr = _context.ContentResolver;
                    var uri = MediaStore.Audio.Artists.ExternalContentUri;

                    const string id = MediaStore.Audio.Artists.InterfaceConsts.Id;
                    const string artist = MediaStore.Audio.Artists.InterfaceConsts.Artist;
                    const string numberOfAlbums = MediaStore.Audio.Artists.InterfaceConsts.NumberOfAlbums;
                    const string numberOfTracks = MediaStore.Audio.Artists.InterfaceConsts.NumberOfTracks;

                    string[] columns = { id, artist, numberOfAlbums, numberOfTracks };

                    using (var cursor = cr.Query(uri, columns, selection, selectionArgs, sortOrder))
                    {
                        int idColumn = cursor.GetColumnIndex(id);
                        int artistColumn = cursor.GetColumnIndex(artist);
                        int numberOfAlbumsColumn = cursor.GetColumnIndex(numberOfAlbums);
                        int numberOfTracksColumn = cursor.GetColumnIndex(numberOfTracks);

                        cursor.MoveToFirst();

                        while (!cursor.IsAfterLast)
                        {
                            string artistId = cursor.GetString(idColumn);
                            string artistName = cursor.GetString(artistColumn);
                            int artistNumberOfAlbums = cursor.GetInt(numberOfAlbumsColumn);
                            int artistNumberOfTracks = cursor.GetInt(numberOfTracksColumn);

                            AndroidArtistItem artistItem = new AndroidArtistItem
                                {
                                    Id = artistId,
                                    Name = artistName,
                                    NumberOfAlbums = artistNumberOfAlbums,
                                    NumberOfTracks = artistNumberOfTracks
                                };

                            artistItems.Add(artistItem);

                            cursor.MoveToNext();
                        }
                    }

                    return artistItems;
                });
        }

        public async Task<List<AndroidGenreItem>> GetGenresAsync(string selection = null, string[] selectionArgs = null, string sortOrder = null)
        {
            return await Task.Run(() =>
                {
                    var genreItems = new List<AndroidGenreItem>();

                    ContentResolver cr = _context.ContentResolver;
                    var uri = MediaStore.Audio.Genres.ExternalContentUri;

                    const string id = MediaStore.Audio.Genres.InterfaceConsts.Id;
                    const string count = MediaStore.Audio.Genres.InterfaceConsts.Count;
                    const string name = MediaStore.Audio.Genres.InterfaceConsts.Name;

                    string[] columns = { id, count, name };

                    using (var cursor = cr.Query(uri, columns, selection, selectionArgs, sortOrder))
                    {
                        int idColumn = cursor.GetColumnIndex(id);
                        int countColumn = cursor.GetColumnIndex(count);
                        int nameColumn = cursor.GetColumnIndex(name);

                        cursor.MoveToFirst();

                        while (!cursor.IsAfterLast)
                        {
                            string genreId = cursor.GetString(idColumn);
                            int genreCount = cursor.GetInt(countColumn);
                            string genreName = cursor.GetString(nameColumn);

                            AndroidGenreItem genreItem = new AndroidGenreItem
                                {
                                    Id = genreId,
                                    Name = genreName,
                                    Count = genreCount
                                };

                            genreItems.Add(genreItem);

                            cursor.MoveToNext();
                        }
                    }

                    return genreItems;
                });
        }
    }
}

