using Android.Graphics;
using Subsonic.Client.Items;

namespace Subsonic.Client.Android
{
    public class AndroidArtistItem : ArtistItem
    {
        public int NumberOfAlbums { get; set; }
        public int NumberOfTracks { get; set; }

        private Bitmap _image;

        public Bitmap Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }
    }
 }

