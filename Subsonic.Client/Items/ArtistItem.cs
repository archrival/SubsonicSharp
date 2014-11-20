using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Subsonic.Common.Classes;
using Subsonic.Client.Common.Properties;
using System.Runtime.CompilerServices;

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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}