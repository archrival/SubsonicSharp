using System;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Activities
{
	public class Search2ActivityDelegate<TImageType> : SubsonicActivityDelegate<SearchResult2, TImageType>
	{
		private string Query { get; set; }
		private int? ArtistCount { get; set; }
		private int? ArtistOffset { get; set; }
		private int? AlbumCount { get; set; }
		private int? AlbumOffset { get; set; }
		private int? SongCount { get; set; }
		private int? SongOffset { get; set; }
		private string MusicFolderId { get; set; }

		public Search2ActivityDelegate(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, string musicFolderId = null)
		{
			Query = query;
			ArtistCount = artistCount;
			ArtistOffset = artistOffset;
			AlbumCount = albumCount;
			AlbumOffset = albumOffset;
			SongCount = songCount;
			SongOffset = songOffset;
			MusicFolderId = musicFolderId;
		}

		public Func<CancellationToken?, Task<SearchResult2>> CreateMethod(ISubsonicClient<TImageType> subsonicClient)
		{
			return cancelToken => subsonicClient.Search2Async(Query, ArtistCount, ArtistOffset, AlbumCount, AlbumOffset, SongCount, SongOffset, MusicFolderId, cancelToken);
		}

		// Overrides for equality
		#region HashCode and Equality Overrides
		private const int HashSeed = 73; // Should be prime number
		private const int HashFactor = 17; // Should be prime number

		public override int GetHashCode()
		{
			int hash = HashSeed;
			hash = (hash * HashFactor) + typeof(Search2ActivityDelegate<TImageType>).GetHashCode();

			if (Query != null)
				hash = (hash * HashFactor) + Query.GetHashCode();

			if (MusicFolderId != null)
				hash = (hash * HashFactor) + MusicFolderId.GetHashCode();

			if (ArtistCount.HasValue)
				hash = (hash * HashFactor) + ArtistCount.Value.GetHashCode();

			if (ArtistOffset.HasValue)
				hash = (hash * HashFactor) + ArtistOffset.Value.GetHashCode();

			if (AlbumCount.HasValue)
				hash = (hash * HashFactor) + AlbumCount.Value.GetHashCode();

			if (AlbumOffset.HasValue)
				hash = (hash * HashFactor) + AlbumOffset.Value.GetHashCode();

			if (SongCount.HasValue)
				hash = (hash * HashFactor) + SongCount.Value.GetHashCode();

			if (SongOffset.HasValue)
				hash = (hash * HashFactor) + SongOffset.Value.GetHashCode();

			return hash;
		}

		public override bool Equals(object obj)
		{
			return obj != null && Equals(obj as Search2ActivityDelegate<TImageType>);
		}

		private bool Equals(Search2ActivityDelegate<TImageType> item)
		{
			return item != null && this == item;
		}

		public static bool operator ==(Search2ActivityDelegate<TImageType> left, Search2ActivityDelegate<TImageType> right)
		{
			if (ReferenceEquals(null, left))
				return ReferenceEquals(null, right);

			if (ReferenceEquals(null, right))
				return false;

			if (left.Query != null)
			if (!left.Query.Equals(right.Query))
				return false;

			if (left.MusicFolderId != null)
			if (!left.MusicFolderId.Equals(right.MusicFolderId))
				return false;

			if (left.ArtistCount.HasValue)
			if (!right.ArtistCount.HasValue)
				return false;
			else if (!left.ArtistCount.Value.Equals(right.ArtistCount.Value))
				return false;

			if (left.ArtistOffset.HasValue)
			if (!right.ArtistOffset.HasValue)
				return false;
			else if (!left.ArtistOffset.Value.Equals(right.ArtistOffset.Value))
				return false;

			if (left.AlbumCount.HasValue)
			if (!right.AlbumCount.HasValue)
				return false;
			else if (!left.AlbumCount.Value.Equals(right.AlbumCount.Value))
				return false;

			if (left.AlbumOffset.HasValue)
			if (!right.AlbumOffset.HasValue)
				return false;
			else if (!left.AlbumOffset.Value.Equals(right.AlbumOffset.Value))
				return false;

			if (left.SongCount.HasValue)
			if (!right.SongCount.HasValue)
				return false;
			else if (!left.SongCount.Value.Equals(right.SongCount.Value))
				return false;

			if (left.SongOffset.HasValue)
			if (!right.SongOffset.HasValue)
				return false;
			else if (!left.SongOffset.Value.Equals(right.SongOffset.Value))
				return false;

			return true;
		}

		public static bool operator !=(Search2ActivityDelegate<TImageType> left, Search2ActivityDelegate<TImageType> right)
		{
			return !(left == right);
		}
		#endregion
	}
}

