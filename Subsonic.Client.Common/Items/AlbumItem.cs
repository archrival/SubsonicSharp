﻿namespace Subsonic.Client.Items
{
    public sealed class AlbumItem : ChildItem
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public int AlbumArtSize { get; set; }
        public int Rating { get; set; }
    }
}