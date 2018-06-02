using Arachnee.InnerCore.EntryProviderBases;
using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;
using Movie = Arachnee.InnerCore.Models.Movie;
using TmdbMovie = TMDbLib.Objects.Movies.Movie;

namespace Arachnee.TmdbProvider.Online
{
    public class OnlineDatabase : EntryProvider
    {
        private readonly TMDbClient _client = new TMDbClient(Constants.ApiKey);
        private readonly TmdbConverter _tmdbConverter = new TmdbConverter();
        
        public OnlineDatabase(ILogger logger) : base(logger)
        {
        }

        public override async Task<IList<SearchResult>> GetSearchResultsAsync(string searchQuery, CancellationToken cancellationToken, IProgress<double> progress = null)
        {
            progress?.Report(0);
            Logger?.LogInfo($"Searching for \"{searchQuery}\"...");

            var res = new SearchContainer<SearchBase>();
            try
            {
                res = await _client.SearchMultiAsync(searchQuery, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                Logger?.LogError($"An error occured while searching for \"{searchQuery}\".", e);
            }

            Logger?.LogInfo($"Search for \"{searchQuery}\" landed {res.Results.Count} results.");

            progress?.Report(0.5);

            var results = new List<SearchResult>();
            foreach (var searchBase in res.Results)
            {
                var searchResult = _tmdbConverter.Convert(searchBase);
                results.Add(searchResult);
            }

            progress?.Report(1);
            return results;
        }

        protected override async Task<Entry> LoadEntryAsync(Id entryId, IProgress<double> progress, CancellationToken cancellationToken)
        {
            progress?.Report(0);

            if (Id.IsNullOrDefault(entryId))
            {
                throw new ArgumentException("The given id was the default id.", nameof(entryId));
            }

            Entry entry = DefaultEntry.Instance;
            
            switch (entryId.Type)
            {
                case IdType.Movie:
                    TmdbMovie tmdbMovie = null;
                    try
                    {
                        tmdbMovie = await _client.GetMovieAsync(entryId.Number, (MovieMethods)4239, cancellationToken);
                    }
                    catch (Exception e)
                    {
                        Logger?.LogError($"An error occured while retrieving {nameof(Movie)} corresponding to {entryId}.", e);
                    }

                    entry = _tmdbConverter.ConvertMovie(tmdbMovie);
                    break;

                case IdType.Artist:
                    Person tmdbPerson = null;
                    try
                    {
                        tmdbPerson = await _client.GetPersonAsync(entryId.Number, (PersonMethods)31, cancellationToken);
                    }
                    catch (Exception e)
                    {
                        Logger?.LogError($"An error occured while retrieving {nameof(Artist)} corresponding to {entryId}.", e);
                    }
                        
                    entry = _tmdbConverter.ConvertPerson(tmdbPerson);
                    break;

                case IdType.TvSeries:
                    TvShow tmdbSeries = null;
                    try
                    {
                        tmdbSeries = await _client.GetTvShowAsync(entryId.Number, (TvShowMethods) 127, cancellationToken: cancellationToken);
                    }
                    catch (Exception e)
                    {
                        Logger?.LogError($"An error occured while retrieving artist corresponding to {entryId}.", e);
                    }

                    entry = _tmdbConverter.ConvertTvSeries(tmdbSeries);
                    break;

                default:
                    Logger?.LogError($"{nameof(IdType)} {entryId.Type.ToString()} is not handled for {entryId}.");
                    break;
            }
            
            progress?.Report(1);
            return entry;
        }
    }
}
