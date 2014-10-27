﻿using Subsonic.Client.Common.Properties;
using Subsonic.Common.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Subsonic.Client.Items
{
    public class ChildItem : INotifyPropertyChanged
    {
        public bool Starred { get; set; }
        public Child Child { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}