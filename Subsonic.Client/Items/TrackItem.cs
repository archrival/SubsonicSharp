using System;
using Subsonic.Common.Classes;

namespace Subsonic.Client.Items
{
    public class TrackItem : ChildItem
    {
        private bool _cached;

        public int DiscNumber { get; set; }
        public int TrackNumber { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public TimeSpan Duration { get; set; }
        public int BitRate { get; set; }
        public string FileName { get; set; }
        public Guid PlaylistGuid { get; set; }
        public TrackItem Source { get; set; }

        public bool Cached
        {
            get { return _cached; }
            set
            {
                _cached = value;
                OnPropertyChanged();
            }
        }

        public static TrackItem Create(Child child, string fileName, bool cached)
        {
            return new TrackItem
            {
                Child = child,
                Artist = child.Artist,
                Duration = TimeSpan.FromSeconds(child.Duration),
                Genre = child.Genre,
                Title = child.Title,
                Album = child.Album,
                TrackNumber = child.Track,
                DiscNumber = child.DiscNumber,
                Year = child.Year,
                BitRate = child.BitRate,
                FileName = fileName,
                Cached = cached,
                Starred = child.Starred != new DateTime(),
                Rating = child.UserRating
            };
        }
    }
}