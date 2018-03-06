using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arachnee.InnerCore.ProviderBases
{
    public interface IEntryProvider
    {
        /// <summary>
        /// Logger of the provider so it is able to log informations and errors.
        /// </summary>
        ILogger Logger { set; }
        
        /// <summary>
        /// Runs a search to get a list of entries corresponding to the given query. 
        /// First item in the list is the best result.
        /// </summary>
        Task<IList<SearchResult>> GetSearchResultsAsync(string searchQuery, CancellationToken cancellationToken, IProgress<double> progress);

        /// <summary>
        /// Gets the entry corresponding to the given id.
        /// </summary>
        Task<Entry> GetEntryAsync(string entryId, CancellationToken cancellationToken, IProgress<double> progress);
    }
}
