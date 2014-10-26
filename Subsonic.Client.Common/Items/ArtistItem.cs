using System.Collections.Generic;
using Subsonic.Common.Classes;

namespace Subsonic.Client.Items
{
    public class ArtistItem
    {
        public string Name { get; set; }
        public ICollection<ArtistItem> Children { get; set; }
        public Artist Artist { get; set; }
    }
}