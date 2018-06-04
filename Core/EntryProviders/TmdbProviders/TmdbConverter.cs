using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using System;
using System.Collections.Generic;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;
using TmdbMovie = TMDbLib.Objects.Movies.Movie;

namespace Arachnee.TmdbProviders
{
    public class TmdbConverter
    {
        private const double MinPopularity = 0.1;

        private readonly DateTime _minMovieReleaseDate = new DateTime(1870, 1, 1);
        private readonly DateTime _minArtistBirthday = new DateTime(1800, 1, 1);
        private readonly DateTime _minTvSeriesFirstAirDate = new DateTime(1930, 1, 1);

        private readonly ILogger _logger;

        public TmdbConverter(ILogger logger)
        {
            _logger = logger;
        }

        public SearchResult Convert(SearchBase searchBase)
        {
            throw new System.NotImplementedException();
        }

        public Entry ConvertMovie(TmdbMovie tmdbMovie)
        {
            // checks
            if (tmdbMovie == null)
            {
                _logger?.LogWarning($"The given TMDb movie was null. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }

            if (string.IsNullOrEmpty(tmdbMovie.Title))
            {
                _logger?.LogWarning($"The given TMDb movie with id {tmdbMovie.Id} had no title. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }

            if (!tmdbMovie.ReleaseDate.HasValue || tmdbMovie.ReleaseDate.Value < _minMovieReleaseDate)
            {
                _logger?.LogWarning($"TMDb movie {tmdbMovie.Id} ({tmdbMovie.Title}) had an invalid {nameof(TmdbMovie.ReleaseDate)}. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }

            if (tmdbMovie.Adult)
            {
                _logger?.LogWarning($"TMDb movie {tmdbMovie.Id} ({tmdbMovie.Title}) was an adult movie. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }

            if (string.IsNullOrEmpty(tmdbMovie.PosterPath))
            {
                _logger?.LogWarning($"TMDb movie {tmdbMovie.Id} ({tmdbMovie.Title})  didn't have a {nameof(TmdbMovie.PosterPath)}. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }

            if (tmdbMovie.Popularity < MinPopularity)
            {
                _logger?.LogWarning($"TMDb movie {tmdbMovie.Id} ({tmdbMovie.Title})  had a low popularity that was less than {MinPopularity}. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }
            
            // build basic values
            var id = Id.FromMovieNumber(tmdbMovie.Id);
            var movie = new Movie(id)
            {
                Title = tmdbMovie.Title,
                Popularity = tmdbMovie.Popularity
            };
            
            // create the connections

            return movie;
        }

        public Entry ConvertPerson(Person tmdbPerson)
        {
            // checks
            if (tmdbPerson == null)
            {
                _logger?.LogWarning($"The given TMDb person was null. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }

            if (string.IsNullOrEmpty(tmdbPerson.Name))
            {
                _logger?.LogWarning($"The given TMDb person with id {tmdbPerson.Id} had no name. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }

            if (!tmdbPerson.Birthday.HasValue || tmdbPerson.Birthday.Value < _minArtistBirthday)
            {
                _logger?.LogWarning($"TMDb person {tmdbPerson.Id} ({tmdbPerson.Name})  had an incorrect {nameof(Person.Birthday)}. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }

            if (string.IsNullOrEmpty(tmdbPerson.ProfilePath))
            {
                _logger?.LogWarning($"TMDb person {tmdbPerson.Id} ({tmdbPerson.Name}) had no {nameof(Person.ProfilePath)}. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }

            if (tmdbPerson.Popularity < MinPopularity)
            {
                _logger?.LogWarning($"TMDb person {tmdbPerson.Id} ({tmdbPerson.Name}) had a low popularity that was less than {MinPopularity}. {nameof(DefaultEntry)} will be returned.");
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
            
            // create the connections
            
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
                _logger?.LogWarning($"The given TMDb tv show was null. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }

            if (string.IsNullOrEmpty(tmdbSeries.Name))
            {
                _logger?.LogWarning($"The given TMDb tv show with id {tmdbSeries.Id} had no name. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }

            if (!tmdbSeries.FirstAirDate.HasValue || tmdbSeries.FirstAirDate.Value < _minTvSeriesFirstAirDate)
            {
                _logger?.LogWarning($"TMDb tv show {tmdbSeries.Id} ({tmdbSeries.Name}) had an invalid {nameof(TvShow.FirstAirDate)}. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }
            
            if (string.IsNullOrEmpty(tmdbSeries.PosterPath))
            {
                _logger?.LogWarning($"TMDb tv show {tmdbSeries.Id} ({tmdbSeries.Name}) didn't have a {nameof(TmdbMovie.PosterPath)}. {nameof(DefaultEntry)} will be returned.");
                return DefaultEntry.Instance;
            }

            if (tmdbSeries.Popularity < MinPopularity)
            {
                _logger?.LogWarning($"TMDb tv show {tmdbSeries.Id} ({tmdbSeries.Name}) had a low popularity that was less than {MinPopularity}. {nameof(DefaultEntry)} will be returned.");
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

            // create the connections

            return tv;
        }
    }
}