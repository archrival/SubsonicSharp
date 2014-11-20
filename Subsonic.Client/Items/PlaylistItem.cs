using Subsonic.Common.Classes;
using System;
using System.ComponentModel;
using Subsonic.Client.Common.Properties;
using System.Runtime.CompilerServices;

namespace Subsonic.Client.Items
{
    public class PlaylistItem : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public int Tracks { get; set; }
        public TimeSpan Duration { get; set; }
        public Playlist Playlist { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
