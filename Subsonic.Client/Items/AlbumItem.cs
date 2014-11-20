﻿using System.ComponentModel;
using Subsonic.Client.Common.Properties;
using System.Runtime.CompilerServices;

namespace Subsonic.Client.Items
{
    public class AlbumItem : ChildItem
    {
        public string Name { get; set; }
        public string Parent { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}