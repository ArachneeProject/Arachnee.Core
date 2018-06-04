using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Objects.People;
using TMDbLib.Objects.TvShows;
using Movie = TMDbLib.Objects.Movies.Movie;

namespace Arachnee.TmdbProviders
{
    // TODO: to improve
    public class OfflineDatabase : TmdbDatabase
    {
        private readonly ConcurrentDictionary<Id, Entry> _offlineDatabase = new ConcurrentDictionary<Id, Entry>();

        public OfflineDatabase(string resourcesFolder, ILogger logger) : base(resourcesFolder, logger)
        {
        }

        public void LoadAll()
        {
            LoadMovies();
            LoadArtists();
            LoadTvSeries();
        }

        public void LoadMovies()
        {
            Logger?.LogDebug("Loading movies...");

            var moviesFile = Path.Combine(ResourcesFolder, $"{nameof(Movie)}.json");

            if (!File.Exists(moviesFile))
            {
                Logger?.LogError($"Movies not found at \"{moviesFile}\".");
                return;
            }

            using (var streamReader = new StreamReader(moviesFile))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var tmdbMovie = JsonConvert.DeserializeObject<Movie>(line);

                    var movie = TmdbConverter.ConvertMovie(tmdbMovie);
                    if (Entry.IsNullOrDefault(movie))
                    {
                        continue;
                    }

                    _offlineDatabase.TryAdd(movie.Id, movie);
                }
            }

            Logger?.LogDebug("Movies loaded.");
        }

        public void LoadArtists()
        {
            var artistsFile = Path.Combine(ResourcesFolder, $"{nameof(Person)}.json");

            if (!File.Exists(artistsFile))
            {
                Logger?.LogError($"People not found at \"{artistsFile}\".");
                return;
            }

            using (var streamReader = new StreamReader(artistsFile))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var tmdbPerson = JsonConvert.DeserializeObject<Person>(line);

                    var artist = TmdbConverter.ConvertPerson(tmdbPerson);
                    if (Entry.IsNullOrDefault(artist))
                    {
                        continue;
                    }

                    _offlineDatabase.TryAdd(artist.Id, artist);
                }
            }
        }

        public void LoadTvSeries()
        {
            var tvSeriesFile = Path.Combine(ResourcesFolder, $"{nameof(TvSeries)}.json");

            if (!File.Exists(tvSeriesFile))
            {
                Logger?.LogError($"TvSeries not found at \"{tvSeriesFile}\".");
                return;
            }

            using (var streamReader = new StreamReader(tvSeriesFile))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var tmdbTvSeries = JsonConvert.DeserializeObject<TvShow>(line);

                    var tvSeries = TmdbConverter.ConvertTvSeries(tmdbTvSeries);
                    if (Entry.IsNullOrDefault(tvSeries))
                    {
                        continue;
                    }

                    _offlineDatabase.TryAdd(tvSeries.Id, tvSeries);
                }
            }
        }

        public override async Task<IList<SearchResult>> GetSearchResultsAsync(string searchQuery, CancellationToken cancellationToken, IProgress<double> progress = null)
        {
            if (FallbackProvider != null)
            {
                return await FallbackProvider.GetSearchResultsAsync(searchQuery, cancellationToken, progress);
            }

            Logger?.LogWarning($"{nameof(OfflineDatabase)} is not able to search for \"{searchQuery}\" " +
                              $"because no {nameof(FallbackProvider)} has been set.");
            return new List<SearchResult>();
        }

        protected override async Task<Entry> LoadEntryAsync(Id entryId, IProgress<double> progress, CancellationToken cancellationToken)
        {
            if (Id.IsNullOrDefault(entryId))
            {
                throw new ArgumentException("The given id was null or default.");
            }
            
            if (_offlineDatabase.ContainsKey(entryId))
            {
                return _offlineDatabase[entryId];
            }

            if (_offlineDatabase.Count == 0)
            {
                Logger?.LogWarning($"{nameof(OfflineDatabase)} is empty. Did you forget to call the {nameof(LoadAll)} method?");
            }

            if (FallbackProvider != null)
            {
                return await FallbackProvider.GetEntryAsync(entryId, cancellationToken, progress);
            }

            Logger?.LogWarning($"{nameof(OfflineDatabase)} is not able to load entry corresponding to \"{entryId}\" " +
                               $"because no {nameof(FallbackProvider)} has been set.");

            return DefaultEntry.Instance;
        }
    }
}