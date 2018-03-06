//using Arachnee.InnerCore.Models;
//using Arachnee.InnerCore.ProviderBases;
//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Assets.Classes.Core.EntryProviders.OnlineDatabase
//{
//    public class OnlineDatabase : EntryProvider
//    {
//        private readonly TmdbProxy _proxy = new TmdbProxy();

//        public override Task<IList<SearchResult>> GetSearchResultsAsync(string searchQuery, CancellationToken cancellationToken, IProgress<double> progress = null)
//        {
//            var queue = new Queue<SearchResult>();
//            if (string.IsNullOrEmpty(searchQuery))
//            {
//                return queue;
//            }

//            var results = _proxy.GetSearchResults(searchQuery);
//            foreach (var searchResult in results)
//            {
//                queue.Enqueue(searchResult);
//            }

//            return queue;
//        }

//        protected override Task<Entry> LoadEntryAsync(string entryId, IProgress<double> progress, CancellationToken cancellationToken)
//        {
//            try
//            {
//                entry = _proxy.GetEntry(entryId);
//                return true;
//            }
//            catch (Exception e)
//            {
//                Logger.LogException(e);
//                entry = DefaultEntry.Instance;
//                return false;
//            }
//        }

//    }
//}
