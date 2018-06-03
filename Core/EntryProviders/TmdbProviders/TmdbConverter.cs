using Arachnee.InnerCore.Models;
using System;
using System.Collections.Generic;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;
using TmdbMovie = TMDbLib.Objects.Movies.Movie;

namespace Arachnee.TmdbProviders
{
    // TODO: Add Logger
    public class TmdbConverter
    {
        private const double MinPopularity = 0.1;

        public SearchResult Convert(SearchBase searchBase)
        {
            throw new System.NotImplementedException();
        }

        public Entry ConvertMovie(TmdbMovie tmdbMovie)
        {
            // checks
            if (tmdbMovie == null)
            {
                return DefaultEntry.Instance;
            }

            if (string.IsNullOrEmpty(tmdbMovie.Title))
            {
                return DefaultEntry.Instance;
            }

            if (!tmdbMovie.ReleaseDate.HasValue || tmdbMovie.ReleaseDate.Value < new DateTime(1870, 1, 1))
            {
                return DefaultEntry.Instance;
            }

            if (tmdbMovie.Adult)
            {
                return DefaultEntry.Instance;
            }

            if (string.IsNullOrEmpty(tmdbMovie.PosterPath))
            {
                return DefaultEntry.Instance;
            }

            if (tmdbMovie.Popularity < MinPopularity)
            {
                return DefaultEntry.Instance;
            }
            
            // build basic values
            var id = Id.FromMovieNumber(tmdbMovie.Id);
            var movie = new Movie(id)
            {
                Title = tmdbMovie.Title,
                Popularity = tmdbMovie.Popularity
            };
            
            // TODO: create the connections

            return movie;
        }

        public Entry ConvertPerson(Person tmdbPerson)
        {
            // checks
            if (tmdbPerson == null)
            {
                return DefaultEntry.Instance;
            }

            if (string.IsNullOrEmpty(tmdbPerson.Name))
            {
                return DefaultEntry.Instance;
            }

            if (!tmdbPerson.Birthday.HasValue)
            {
                return DefaultEntry.Instance;
            }

            if (string.IsNullOrEmpty(tmdbPerson.ProfilePath))
            {
                return DefaultEntry.Instance;
            }

            if (tmdbPerson.Popularity < MinPopularity)
            {
                return DefaultEntry.Instance;
            }

            // build basic values
            var id = Id.FromArtistNumber(tmdbPerson.Id);
            var artist = new Artist(id)
            {
                Biography = tmdbPerson.Biography,
                Birthday = tmdbPerson.Birthday.Value,
                Deathday = tmdbPerson.Deathday,
                ImdbId = tmdbPerson.ImdbId,
                MainImagePath = tmdbPerson.ProfilePath,
                Name = tmdbPerson.Name,
                NickNames = new List<string>(tmdbPerson.AlsoKnownAs),
                Popularity = tmdbPerson.Popularity,
            };
            
            // TODO: create the connections
            
            //foreach (var cast in tmdbPerson.MovieCredits.Cast.Where(c => c.!string.IsNullOrEmpty(c.PosterPath)))
            //{
            //    var id = cast.MediaType == "tv"
            //        ? nameof(TvSeries) + IdSeparator + cast.Id
            //        : nameof(Movie) + IdSeparator + cast.Id;

            //    artist.Connections.Add(new Connection
            //    {
            //        ConnectedId = id,
            //        Type = ConnectionType.Actor,
            //        Label = cast.Character
            //    });
            //}

            //foreach (var cast in tmdbPerson.CombinedCredits.Crew.Where(c => !string.IsNullOrEmpty(c.PosterPath)))
            //{
            //    ConnectionType type;
            //    if (_handledCrewJobs.TryGetValue(cast.Job, out type))
            //    {
            //        artist.Connections.Add(new Connection
            //        {
            //            ConnectedId = nameof(Movie) + IdSeparator + cast.Id,
            //            Type = type,
            //            Label = cast.Job
            //        });
            //    }
            //    else
            //    {
            //        artist.Connections.Add(new Connection
            //        {
            //            ConnectedId = nameof(Movie) + IdSeparator + cast.Id,
            //            Type = ConnectionType.Crew,
            //            Label = cast.Job
            //        });
            //    }
            //}

            return artist;
        }

        public Entry ConvertTvSeries(TvShow tmdbSeries)
        {
            // checks
            if (tmdbSeries == null)
            {
                return DefaultEntry.Instance;
            }

            if (string.IsNullOrEmpty(tmdbSeries.Name))
            {
                return DefaultEntry.Instance;
            }

            if (!tmdbSeries.FirstAirDate.HasValue || tmdbSeries.FirstAirDate.Value < new DateTime(1930, 1, 1))
            {
                return DefaultEntry.Instance;
            }
            
            if (string.IsNullOrEmpty(tmdbSeries.PosterPath))
            {
                return DefaultEntry.Instance;
            }

            if (tmdbSeries.Popularity < MinPopularity)
            {
                return DefaultEntry.Instance;
            }

            // build basic values
            var id = Id.FromTvSeriesNumber(tmdbSeries.Id);
            var tv = new TvSeries(id)
            {
                FirstAirDate = tmdbSeries.FirstAirDate.Value,
                MainImagePath = tmdbSeries.PosterPath,
                Name = tmdbSeries.Name,
                Popularity = tmdbSeries.Popularity,
            };

            // TODO: create the connections

            return tv;
        }
    }
}