using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using Arachnee.InnerCore.ProviderBases;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;

namespace Arachnee.InnerCore.Tests.ProviderBases.Tests
{
    [TestFixture]
    public class SampleProviderTests
    {
        private const string MovieId = "Movie-280";
        private const string ArtistId = "Artist-1100";
        private const string MovieTitle = "Terminator 2: Judgment Day";

        private readonly ILogger _logger = new ConsoleLogger();

        private IProgress<double> CreateProgress()
        {
            return new Progress<double>(v => _logger.LogInfo($"Request progress: {100 * v}%."));
        }

        private CancellationToken CreateCancellationToken()
        {
            return new CancellationToken(false);
        }

        [Test]
        public void GetEntryAsync_ValidId_ReturnsValidEntry()
        {
            var provier = new SampleProvider { Logger = _logger };
            
            var entry = provier.GetEntryAsync(MovieId, CreateProgress(), CreateCancellationToken()).Result;
            var movie = entry as Movie;

            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            Assert.AreEqual(MovieId, entry.Id);
            Assert.IsNotNull(movie);
            Assert.AreEqual(MovieTitle, movie.Title);
        }

        [Test]
        public void GetEntryAsync_EmptyId_ReturnsDefaultEntry()
        {
            var provier = new SampleProvider { Logger = _logger };

            var entry = provier.GetEntryAsync(string.Empty, CreateProgress(), CreateCancellationToken()).Result;

            Assert.IsTrue(Entry.IsNullOrDefault(entry));
        }

        [Test]
        public void GetEntryAsync_NullId_ReturnsDefaultEntry()
        {
            var provier = new SampleProvider { Logger = _logger };

            var entry = provier.GetEntryAsync(null, CreateProgress(), CreateCancellationToken()).Result;

            Assert.IsTrue(Entry.IsNullOrDefault(entry));
        }

        [Test, Description("A null IProgress shouldn't have any impact on the function.")]
        public void GetEntryAsync_ValidIdAndNullProgress_ReturnsValidEntry()
        {
            var provier = new SampleProvider { Logger = _logger };

            var entry = provier.GetEntryAsync(MovieId, null, CreateCancellationToken()).Result;

            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            Assert.AreEqual(MovieId, entry.Id);
        }

        [Test]
        public void GetConnectedEntriesAsync_ValidId_ReturnsConnectedEntries()
        {
            var provier = new SampleProvider { Logger = _logger };

            var connectedEntries = provier.GetConnectedEntriesAsync<Entry>(MovieId, Connection.AllTypes(), CreateProgress(), CreateCancellationToken()).Result.ToList();

            Assert.AreEqual(2, connectedEntries.Count);
            Assert.AreEqual(ArtistId, connectedEntries[1].Id);
        }

        [Test]
        public void GetSearchResultsAsync_ValidQuery_ReturnsValidResult()
        {
            var provier = new SampleProvider { Logger = _logger };

            var searchResults = provier.GetSearchResultsAsync(MovieTitle, CreateProgress(), CreateCancellationToken()).Result;

            Assert.AreEqual(1, searchResults.Count);
            Assert.AreEqual(MovieId, searchResults.First().EntryId);
            Assert.AreEqual(SearchResultType.Movie, searchResults.First().SearchResultType);
        }

        [Test]
        public void GetSearchResultsAsync_ValidQueryAndNullProgress_ReturnsValidResult()
        {
            var provier = new SampleProvider { Logger = _logger };
            
            var searchResults = provier.GetSearchResultsAsync(MovieTitle, null, CreateCancellationToken()).Result;

            Assert.AreEqual(1, searchResults.Count);
            Assert.AreEqual(MovieId, searchResults.First().EntryId);
            Assert.AreEqual(SearchResultType.Movie, searchResults.First().SearchResultType);
        }
    }
}