using Subsonic.Common.Classes;
using System;

namespace Subsonic.Client.Models
{
    public class AlbumModel : ChildModel
    {
        public string Name { get; set; }
        public string Parent { get; set; }

        public static AlbumModel Create(Child child)
        {
            return new AlbumModel
            {
                Child = child,
                Name = child.Album,
                Parent = child.Parent,
                Artist = child.Artist,
                Genre = child.Genre,
                Year = child.Year,
                Starred = child.Starred != new DateTime(),
                Rating = child.UserRating
            };
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as AlbumModel);
        }

        private bool Equals(AlbumModel item)
        {
            return item != null && this == item;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            int hashFactor = 7;

            hash = (hash * hashFactor) + base.GetHashCode();
            hash = (hash * hashFactor) + Name.GetHashCode();
            hash = (hash * hashFactor) + Parent.GetHashCode();

            return hash;
        }

        public static bool operator ==(AlbumModel left, AlbumModel right)
        {
            if (ReferenceEquals(null, left))
                return ReferenceEquals(null, right);

            if (ReferenceEquals(null, right))
                return false;

            if (!string.Equals(left.Name, right.Name))
                return false;

            if (!string.Equals(left.Parent, right.Parent))
                return false;

            return true;
        }

        public static bool operator !=(AlbumModel left, AlbumModel right)
        {
            return !(left == right);
        }
    }
}