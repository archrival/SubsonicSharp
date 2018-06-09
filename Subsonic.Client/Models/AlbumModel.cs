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

        public static bool operator !=(AlbumModel left, AlbumModel right)
        {
            return !(left == right);
        }

        public static bool operator ==(AlbumModel left, AlbumModel right)
        {
            if (left is null)
                return right is null;

            if (right is null)
                return false;

            if (!string.Equals(left.Name, right.Name))
                return false;

            if (!string.Equals(left.Parent, right.Parent))
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as AlbumModel);
        }

        public override int GetHashCode()
        {
            var hash = 13;
            var hashFactor = 7;

            hash = hash * hashFactor + base.GetHashCode();
            hash = hash * hashFactor + Name.GetHashCode();
            hash = hash * hashFactor + Parent.GetHashCode();

            return hash;
        }

        private bool Equals(AlbumModel item)
        {
            return item != null && this == item;
        }
    }
}