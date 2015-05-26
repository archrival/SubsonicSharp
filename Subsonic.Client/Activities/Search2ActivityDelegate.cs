﻿using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client
{
	public class Search2ActivityDelegate<TImageType> : SubsonicActivityDelegate<SearchResult2, TImageType>
	{
		public string Query { get; set; }
		public int? ArtistCount { get; set; }
		public int? ArtistOffset { get; set; }
		public int? AlbumCount { get; set; }
		public int? AlbumOffset { get; set; }
		public int? SongCount { get; set; }
		public int? SongOffset { get; set; }
		public string MusicFolderId { get; set; }

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

		public void CreateFunction(ISubsonicClient<TImageType> subsonicClient)
		{
			Method = (cancelToken) => subsonicClient.Search2Async(Query, ArtistCount, ArtistOffset, AlbumCount, AlbumOffset, SongCount, SongOffset, MusicFolderId, cancelToken);
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

		private bool Equals(Search2ActivityDelegate<TImageType> search2Activity)
		{
			return search2Activity != null && this == search2Activity;
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
