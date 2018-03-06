using System;

namespace Arachnee.EntryProviders.TmdbProvider.Tmdb.TmdbObjects
{
    public class Season
    {
        public DateTime AirDate { get; set; }
        public long EpisodeCount { get; set; }
        public long Id { get; set; }
        public string PosterPath { get; set; }
        public long SeasonNumber { get; set; }
    }
}