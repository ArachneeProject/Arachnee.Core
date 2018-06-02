using System;
using System.Collections.Generic;

namespace Arachnee.InnerCore.Models
{
    public class Movie : Entry
    {
        public string BackdropPath { get; set; }

        public long Budget { get; set; }

        public string Homepage { get; set; }

        public string ImdbId { get; set; }

        public string OriginalLanguage { get; set; }

        public string OriginalTitle { get; set; }

        public string Overview { get; set; }

        public double Popularity { get; set; }

        public DateTime ReleaseDate { get; set; }

        public long Revenue { get; set; }

        public TimeSpan Runtime { get; set; }

        public ReleaseStatus Status { get; set; }

        public string Tagline { get; set; }

        public List<string> Tags { get; set; }

        public string Title { get; set; }

        public double VoteAverage { get; set; }

        public int VoteCount { get; set; }

        public Movie(Id id) : base(id)
        {
        }

        public override string ToString()
        {
            return $"{Title} ({Id})";
        }
    }
}