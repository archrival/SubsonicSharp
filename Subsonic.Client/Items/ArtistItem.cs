using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Subsonic.Common.Classes;

namespace Subsonic.Client.Items
{
    public class ArtistItem : INotifyPropertyChanged
    {
        public ArtistItem()
        {
            Children = new ObservableCollection<ArtistItem>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ArtistItem> Children { get; set; }
        public Artist Artist { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}