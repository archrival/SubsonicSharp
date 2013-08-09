using System.Windows.Media.Imaging;

namespace Subsonic.Client.Common.Items
{
    public sealed class AlbumItem : ChildItem
    {
        private BitmapSource _image;

        public string Name { get; set; }
        public string Artist { get; set; }
        public int AlbumArtSize { get; set; }
        public int Rating { get; set; }

        public BitmapSource Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }
    }
}