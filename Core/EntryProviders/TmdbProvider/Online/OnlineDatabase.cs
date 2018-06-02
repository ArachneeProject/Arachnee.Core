using Arachnee.InnerCore.EntryProviderBases;
using Arachnee.InnerCore.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Client;

namespace Arachnee.TmdbProvider.Online
{
    public class OnlineDatabase : EntryProvider
    {
        private readonly TMDbClient _client = new TMDbClient(Constants.ApiKey);
        private readonly TmdbConverter _converter = new TmdbConverter();

        public override async Task<IList<SearchResult>> GetSearchResultsAsync(string searchQuery, CancellationToken cancellationToken, IProgress<double> progress = null)
        {
            progress?.Report(0);
            
            var res = await _client.SearchMultiAsync(searchQuery, cancellationToken: cancellationToken);
            
            progress?.Report(0.5);

            var results = new List<SearchResult>();
            foreach (var searchBase in res.Results)
            {
                var searchResult = _converter.Convert(searchBase);
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
                throw new ArgumentException("Entry id was the default id.", nameof(entryId));
            }

            //try
            //{
            //    switch (entryId.Type)
            //    {
            //        case nameof(Movie):
            //            crawledEntity = _client.GetMovieAsync(id, (MovieMethods)4239).Result;
            //            break;

            //        case EntityType.Person:
            //            crawledEntity = _client.GetPersonAsync(id, (PersonMethods)31).Result;
            //            break;

            //        case EntityType.TvSeries:
            //            crawledEntity = _client.GetTvShowAsync(id, (TvShowMethods)127).Result;
            //            break;

            //        case EntityType.Collection:
            //            crawledEntity = _client.GetCollectionAsync(id, CollectionMethods.Images).Result;
            //            break;

            //        case EntityType.Keyword:
            //            crawledEntity = _client.GetKeywordAsync(id).Result;
            //            break;

            //        default:
            //            _logger.LogError($"EntityType {entityType} is not handled.");
            //            break;
            //    }
            //}
            //catch (Exception e)
            //{
            //    _logger.LogException(e);
            //}
            throw new NotImplementedException();
        }
    }
}
