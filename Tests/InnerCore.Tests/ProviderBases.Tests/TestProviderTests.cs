using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using Arachnee.InnerCore.ProviderBases;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arachnee.InnerCore.Tests.ProviderBases.Tests
{
    [TestFixture]
    public class TestProviderTests
    {
        private const string MovieTitle = "Terminator 2: Judgment Day";

        private readonly ILogger _logger = new ConsoleLogger();

        private TestProvider _provider;
        
        private IProgress<double> CreateProgress()
        {
            return new Progress<double>(v => _logger.LogInfo($"Request progress: {100 * v}%."));
        }

        private CancellationToken CreateCancellationToken()
        {
            return new CancellationToken(false);
        }

        [SetUp]
        public void SetUp()
        {
            _provider = new TestProvider { Logger = _logger };
        }

        [Test]
        public void GetEntryAsync_ValidId_ReturnsValidEntry()
        {
            var entry = _provider.GetEntryAsync(TestProvider.Terminator2JudgmentDayId, CreateProgress(), CreateCancellationToken()).Result;
            var movie = entry as Movie;

            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            Assert.AreEqual(TestProvider.Terminator2JudgmentDayId, entry.Id);
            Assert.IsNotNull(movie);
            Assert.AreEqual(MovieTitle, movie.Title);
        }

        [Test]
        public void GetEntryAsync_EmptyId_ReturnsDefaultEntry()
        {
            var entry = _provider.GetEntryAsync(string.Empty, CreateProgress(), CreateCancellationToken()).Result;

            Assert.IsTrue(Entry.IsNullOrDefault(entry));
        }

        [Test]
        public void GetEntryAsync_NullId_ReturnsDefaultEntry()
        {
            var entry = _provider.GetEntryAsync(null, CreateProgress(), CreateCancellationToken()).Result;

            Assert.IsTrue(Entry.IsNullOrDefault(entry));
        }

        [Test]
        public void GetEntryAsync_MultiThread_ReturnCachedEntry()
        {
            _provider.Logger = null; // TODO: disable logger [ https://github.com/SuperValou/Arachnee.Core/projects/2#card-7942481 ]

            var dictionary = new ConcurrentDictionary<Entry, int>();
            var random = new Random();
            Parallel.ForEach(new byte[1000], b =>
            {
                int delay = random.Next(100);
                Task.Delay(delay);
                dictionary.TryAdd(_provider.GetEntryAsync(TestProvider.Terminator2JudgmentDayId, new Progress<double>(), CreateCancellationToken()).Result, delay);
            });
            
            Assert.AreEqual(1, dictionary.Count);
            Assert.IsFalse(Entry.IsNullOrDefault(dictionary.First().Key));
            Assert.AreEqual(TestProvider.Terminator2JudgmentDayId, dictionary.First().Key.Id);
        }

        [Test, Description("A null IProgress shouldn't have any impact on the function.")]
        public void GetEntryAsync_ValidIdAndNullProgress_ReturnsValidEntry()
        {
            var entry = _provider.GetEntryAsync(TestProvider.Terminator2JudgmentDayId, null, CreateCancellationToken()).Result;

            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            Assert.AreEqual(TestProvider.Terminator2JudgmentDayId, entry.Id);
        }

        [Test]
        public void GetConnectedEntriesAsync_ValidId_ReturnsConnectedEntries()
        {
            var connectedEntries = _provider.GetConnectedEntriesAsync<Entry>(TestProvider.Terminator2JudgmentDayId, Connection.AllTypes(), CreateProgress(), CreateCancellationToken()).Result.ToList();

            Assert.AreEqual(2, connectedEntries.Count);
            Assert.AreEqual(TestProvider.ArnoldSchwarzeneggerId, connectedEntries[1].Id);
        }

        [Test]
        public void GetSearchResultsAsync_ValidQuery_ReturnsValidResult()
        {
            var searchResults = _provider.GetSearchResultsAsync(MovieTitle, CreateProgress(), CreateCancellationToken()).Result;

            Assert.AreEqual(1, searchResults.Count);
            Assert.AreEqual(TestProvider.Terminator2JudgmentDayId, searchResults.First().EntryId);
            Assert.AreEqual(SearchResultType.Movie, searchResults.First().SearchResultType);
        }

        [Test]
        public void GetSearchResultsAsync_ValidQueryAndNullProgress_ReturnsValidResult()
        {
            var searchResults = _provider.GetSearchResultsAsync(MovieTitle, null, CreateCancellationToken()).Result;

            Assert.AreEqual(1, searchResults.Count);
            Assert.AreEqual(TestProvider.Terminator2JudgmentDayId, searchResults.First().EntryId);
            Assert.AreEqual(SearchResultType.Movie, searchResults.First().SearchResultType);
        }
    }
}