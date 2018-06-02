using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arachnee.InnerCore.EntryProviderBases
{
    public abstract class EntryProvider : IEntryProvider
    {
        protected readonly Cache<Id, Entry> CachedEntries = new Cache<Id, Entry>();

        public IEntryProvider FallbackProvider { get; set; }
        
        public ILogger Logger { get; set; }

        protected EntryProvider(ILogger logger)
        {
            Logger = logger;
        }

        public abstract Task<IList<SearchResult>> GetSearchResultsAsync(string searchQuery, CancellationToken cancellationToken, IProgress<double> progress = null);
        
        public async Task<Entry> GetEntryAsync(Id entryId, CancellationToken cancellationToken, IProgress<double> progress = null)
        {
            Logger?.LogDebug($"Requesting entry \"{entryId}\"...");
            progress?.Report(0);

            if (Id.IsNullOrDefault(entryId))
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
            
            entry = CachedEntries.GetOrAdd(entryId, entry);

            Logger?.LogDebug($"Entry \"{entryId}\" found: {entry}.");
            progress?.Report(1);
            return entry;
        }

        /// <summary>
        /// Gets all entries connected to the given entry id by at least one of the given connection type. 
        /// </summary>
        public async Task<ICollection<TEntry>> GetConnectedEntriesAsync<TEntry>(Entry entry, CancellationToken cancellationToken, IProgress<double> progress = null,
            ICollection<ConnectionType> connectionTypes = null, Action<TEntry> onConnectedEntryFound = null)
            where TEntry : Entry
        {
            if (connectionTypes == null)
            {
                connectionTypes = Connection.AllTypes();
            }

            Logger?.LogDebug($"Requesting {typeof(TEntry).Name} connections \"{string.Join(",", connectionTypes)}\" on \"{entry}\"...");
            progress?.Report(0);
            
            if (Entry.IsNullOrDefault(entry))
            {
                Logger?.LogError("The given entry was null or the default entry.");
                progress?.Report(1);
                return new List<TEntry>();
            }
            
            var oppositeEntries = new Dictionary<Id, TEntry>();
            var validConnections = entry.Connections.Where(c => connectionTypes.Contains(c.Type)).ToList();
            
            int progressCount = -1;
            foreach (var connection in validConnections)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Logger?.LogDebug($"Cancellation requested for connections of {entry}.");
                    break;
                }

                progressCount++;
                double count = progressCount;
                IProgress<double> subProgress = new Progress<double>(value => 
                    progress?.Report(count / validConnections.Count + value / validConnections.Count));

                if (oppositeEntries.ContainsKey(connection.ConnectedId))
                {
                    subProgress.Report(1);
                    continue;
                }

                var oppositeEntry = await GetEntryAsync(connection.ConnectedId, cancellationToken, subProgress);

                if (Entry.IsNullOrDefault(oppositeEntry))
                {
                    continue;
                }

                var oppositeTEntry = oppositeEntry as TEntry;
                if (oppositeTEntry == null)
                {
                    continue;
                }

                Logger?.LogDebug($"{entry} -- {connection.Type} --> {oppositeTEntry}.");
                oppositeEntries.Add(oppositeTEntry.Id, oppositeTEntry);

                onConnectedEntryFound?.Invoke(oppositeTEntry);
            }

            return oppositeEntries.Values.ToList();
        }

        protected abstract Task<Entry> LoadEntryAsync(Id entryId, IProgress<double> progress, CancellationToken cancellationToken);
    }
}
