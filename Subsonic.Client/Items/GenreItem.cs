using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Subsonic.Client.Items
{
    public class GenreItem
    {
        public string Name { get; set; }
        public int AlbumCount { get; set; }
        public int SongCount { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

