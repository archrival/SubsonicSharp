using Android.Graphics;
using Subsonic.Client.Items;

namespace Subsonic.Client.Android
{
    public class AndroidChildItem : ChildItem
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

