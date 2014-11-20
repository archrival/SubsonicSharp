using Subsonic.Client.Items;
using Android.Graphics;

namespace Subsonic.Client.Android
{
    public class AndroidTrackItem : TrackItem
    {
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

