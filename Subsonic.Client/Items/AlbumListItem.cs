using Subsonic.Common.Enums;
using System.ComponentModel;

namespace Subsonic.Client.Items
{
    public sealed class AlbumListItem : INotifyPropertyChanged
    {
        public AlbumListType AlbumListType { get; set; }
        public int Current { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}