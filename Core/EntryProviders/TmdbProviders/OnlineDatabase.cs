using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Arachnee.TmdbProviders
{
    public class OnlineDatabase : TmdbDatabase
    {
        private const string ApiKeyFileName = "key";

        private readonly TMDbClient _client;
        
        public OnlineDatabase(string resourcesFolder, ILogger logger) : base(resourcesFolder, logger)
        {
            var apiKeyFilePath = Path.Combine(ResourcesFolder, ApiKeyFileName);
            if (!File.Exists(apiKeyFilePath))
            {
                throw new FileNotFoundException($"API key was not found at \"{apiKeyFilePath}\".");
            }

            var key = File.ReadAllText(apiKeyFilePath);
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"API key not found inside file at \"{apiKeyFilePath}\".");
            }

            _client = new TMDbClient(key);
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
                var searchResult = TmdbConverter.Convert(searchBase);
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
                    TmdbMovie tmdbMovie;
                    try
                    {
                        tmdbMovie = await _client.GetMovieAsync(entryId.Number, (MovieMethods)4239, cancellationToken);
                    }
                    catch (Exception e)
                    {
                        Logger?.LogError($"An error occured while retrieving {nameof(Movie)} corresponding to {entryId}.", e);
                        break;
                    }

                    entry = TmdbConverter.ConvertMovie(tmdbMovie);
                    break;

                case IdType.Artist:
                    Person tmdbPerson;
                    try
                    {
                        tmdbPerson = await _client.GetPersonAsync(entryId.Number, (PersonMethods)31, cancellationToken);
                    }
                    catch (Exception e)
                    {
                        Logger?.LogError($"An error occured while retrieving {nameof(Artist)} corresponding to {entryId}.", e);
                        break;
                    }
                        
                    entry = TmdbConverter.ConvertPerson(tmdbPerson);
                    break;

                case IdType.TvSeries:
                    TvShow tmdbSeries;
                    try
                    {
                        tmdbSeries = await _client.GetTvShowAsync(entryId.Number, (TvShowMethods) 127, cancellationToken: cancellationToken);
                    }
                    catch (Exception e)
                    {
                        Logger?.LogError($"An error occured while retrieving artist corresponding to {entryId}.", e);
                        break;
                    }

                    entry = TmdbConverter.ConvertTvSeries(tmdbSeries);
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
