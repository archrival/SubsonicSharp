using System.ComponentModel;
using System.Runtime.CompilerServices;
using Subsonic.Common.Enums;

namespace Subsonic.Client.Items
{
    public class AlbumListItem : INotifyPropertyChanged
    {
        public AlbumListType AlbumListType { get; set; }
        public int Current { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}