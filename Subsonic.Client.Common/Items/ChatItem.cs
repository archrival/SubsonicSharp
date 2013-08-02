using System;
using System.ComponentModel;

namespace Subsonic.Client.Common.Items
{
    public sealed class ChatItem : INotifyPropertyChanged
    {
        public string User { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}