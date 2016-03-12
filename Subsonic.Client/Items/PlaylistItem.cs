using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Subsonic.Common.Classes;

namespace Subsonic.Client.Items
{
    public class PlaylistItem : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public int Tracks { get; set; }
        public TimeSpan Duration { get; set; }
        public Playlist Playlist { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
