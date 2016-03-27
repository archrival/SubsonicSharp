using System.Collections.Generic;
using System.Collections.ObjectModel;
using Subsonic.Common.Classes;
using Subsonic.Client.Tasks;

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