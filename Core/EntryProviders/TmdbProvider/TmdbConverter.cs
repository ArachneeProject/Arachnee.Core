using Arachnee.InnerCore.Models;
using System;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;
using TmdbMovie = TMDbLib.Objects.Movies.Movie;

namespace Arachnee.TmdbProvider
{
    public class TmdbConverter
    {
        public SearchResult Convert(SearchBase searchBase)
        {
            throw new System.NotImplementedException();
        }

        public Movie ConvertMovie(TmdbMovie tmdbMovie)
        {
            if (tmdbMovie == null)
            {
                throw new ArgumentNullException(nameof(tmdbMovie));
            }

            var id = Id.FromMovieNumber(tmdbMovie.Id);
            var movie = new Movie(id)
            {
                Title = tmdbMovie.Title
            };

            return movie;
        }

        public Artist ConvertPerson(Person tmdbPerson)
        {
            throw new System.NotImplementedException();
        }

        public TvSeries ConvertTvSeries(TvShow tmdbSeries)
        {
            throw new System.NotImplementedException();
        }
    }
}