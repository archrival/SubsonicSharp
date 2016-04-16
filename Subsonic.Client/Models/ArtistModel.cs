using Subsonic.Client.Tasks;
using Subsonic.Common.Classes;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Subsonic.Client.Models
{
    public class ArtistModel : ObservableObject
    {
        public ArtistModel()
        {
            Children = new ObservableCollection<ArtistModel>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ArtistModel> Children { get; set; }
        public Artist Artist { get; set; }
    }
}