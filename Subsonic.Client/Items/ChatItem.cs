using System;
using System.ComponentModel;
using Subsonic.Client.Common.Properties;
using System.Runtime.CompilerServices;

namespace Subsonic.Client.Items
{
    public class ChatItem : INotifyPropertyChanged
    {
        public string User { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}