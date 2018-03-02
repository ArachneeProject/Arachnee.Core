using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arachnee.InnerCore.ProviderBases
{
    public abstract class CacheEntryProvider : IEntryProvider
    {
        private const double SeedProgress = 0.1;

        protected readonly Cache<string, Entry> CachedEntries = new Cache<string, Entry>();

        public IEntryProvider FallbackProvider { get; set; }

        public ILogger Logger { get; set; }

        public abstract Task<IList<SearchResult>> GetSearchResultsAsync(string searchQuery, IProgress<double> progress, CancellationToken cancellationToken);
        
        public async Task<Entry> GetEntryAsync(string entryId, IProgress<double> progress, CancellationToken cancellationToken)
        {
            Logger?.LogDebug($"Requesting entry \"{entryId}\"...");
            progress?.Report(0);

            if (string.IsNullOrEmpty(entryId))
            {
                Logger?.LogError("Unable to provide an entry because the given id was empty.");
                progress?.Report(1);
                return DefaultEntry.Instance;
            }

            if (cancellationToken.IsCancellationRequested)
            {
                Logger?.LogDebug($"Request of entry \"{entryId}\" was cancelled.");
                progress?.Report(1);
                return DefaultEntry.Instance;
            }

            Entry entry;
            if (CachedEntries.TryGetValue(entryId, out entry))
            {
                Logger?.LogDebug($"Entry \"{entryId}\" found in cache: {entry}.");
                progress?.Report(1);
                return entry;
            }

            entry = await LoadEntryAsync(entryId, progress, cancellationToken);
            if (Entry.IsNullOrDefault(entry))
            {
                Logger?.LogWarning($"Entry \"{entryId}\" not found, default entry will be returned.");
                progress?.Report(1);
                return DefaultEntry.Instance;
            }
            
            CachedEntries.AddOrUpdate(entryId, entry);

            Logger?.LogDebug($"Entry \"{entryId}\" found: {entry}.");
            progress?.Report(1);
            return entry;
        }

        /// <summary>
        /// Gets all entries connected to the given entry id by at least one of the given connection type. 
        /// </summary>
        public async Task<ICollection<TEntry>> GetConnectedEntriesAsync<TEntry>(string entryId, ICollection<ConnectionType> connectionTypes, IProgress<double> progress, CancellationToken cancellationToken)
            where TEntry : Entry
        {
            Logger?.LogDebug($"Requesting {typeof(TEntry).Name} connections \"{string.Join(",", connectionTypes)}\" on \"{entryId}\"...");
            progress?.Report(0);

            var entry = await GetEntryAsync(entryId, new Progress<double>(value => progress?.Report(value / SeedProgress)), cancellationToken);
            if (Entry.IsNullOrDefault(entry))
            {
                Logger?.LogDebug($"No connection returned for \"{entryId}\".");
                progress?.Report(1);
                return new List<TEntry>();
            }
            
            var oppositeEntries = new List<Entry>();
            var validConnections = entry.Connections.Where(c => connectionTypes.Contains(c.Type)).ToList();
            int progressCount = 0;
            foreach (var connection in validConnections)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Logger?.LogDebug($"Cancellation requested for connections of {entry}.");
                    break;
                }

                progressCount++;
                var count = progressCount;
                var subProgress = new Progress<double>(value => progress?.Report(SeedProgress + count * (1 - SeedProgress) / validConnections.Count));

                var oppositeEntry = await GetEntryAsync(connection.ConnectedId, subProgress, cancellationToken);

                if (Entry.IsNullOrDefault(oppositeEntry))
                {
                    continue;
                }

                Logger?.LogDebug($"Connected to {entry}: {oppositeEntry}.");
                oppositeEntries.Add(oppositeEntry);
            }

            return oppositeEntries.OfType<TEntry>().ToList();
        }

        protected abstract Task<Entry> LoadEntryAsync(string entryId, IProgress<double> progress, CancellationToken cancellationToken);
    }
}
