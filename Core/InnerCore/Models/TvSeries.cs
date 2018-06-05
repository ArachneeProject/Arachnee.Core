using System;
using System.Collections.Generic;

namespace Arachnee.InnerCore.Models
{
    public class TvSeries : Entry
    {
        public string BackdropPath { get; set; }
        public List<TimeSpan> EpisodeRunTime { get; set; }
        public DateTime FirstAirDate { get; set; }
        public bool InProduction { get; set; }
        public DateTime? LastAirDate { get; set; }
        public string Name { get; set; }
        public int NumberOfEpisodes { get; set; }
        public int NumberOfSeasons { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalName { get; set; }
        public List<string> OriginCountry { get; set; }
        public string Overview { get; set; }
        public double Popularity { get; set; }
        public string PosterPath { get; set; }
        public string Status { get; set; }
        public double VoteAverage { get; set; }
        public long VoteCount { get; set; }
        public List<string> Genres { get; set; }

        public TvSeries(Id id) : base(id)
        {
        }

        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}