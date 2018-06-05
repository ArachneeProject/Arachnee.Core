using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Arachnee.TmdbProviders.Tests
{
    [TestFixture]
    public class OfflineDatabaseTests : TmdbDatabaseTests
    {
        [Test]
        public async Task GetEntryAsync_MovieId_ReturnsCorrectMovie()
        {
            var resourcesFolder = GetResourceFolder();
            var offlineDb = new OfflineDatabase(resourcesFolder, new ConsoleLogger());
            offlineDb.LoadMovies();
            var task = offlineDb.GetEntryAsync(Id.FromMovieNumber(280), CancellationToken.None, new Progress<double>());

            var entry = await task;

            Assert.IsFalse(task.IsFaulted);
            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            var movie = entry as Movie;

            AssertMovie(movie);
        }

        [Test]
        public async Task GetEntryAsync_ArtistId_ReturnsCorrectArtist()
        {
            var resourcesFolder = GetResourceFolder();
            var offlineDb = new OfflineDatabase(resourcesFolder, new ConsoleLogger());
            offlineDb.LoadArtists();
            var task = offlineDb.GetEntryAsync(Id.FromArtistNumber(1100), CancellationToken.None, new Progress<double>());
            var entry = await task;

            Assert.IsFalse(task.IsFaulted);
            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            var artist = entry as Artist;
            
            AssertArtist(artist);
        }

        [Test]
        public async Task GetEntryAsync_TvSeriesId_ReturnsCorrectTvSeries()
        {
            var resourcesFolder = GetResourceFolder();
            var offlineDb = new OfflineDatabase(resourcesFolder, new ConsoleLogger());
            offlineDb.LoadTvSeries();
            var task = offlineDb.GetEntryAsync(Id.FromTvSeriesNumber(1668), CancellationToken.None, new Progress<double>());
            var entry = await task;

            Assert.IsFalse(task.IsFaulted);
            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            var tv = entry as TvSeries;
            
            AssertTvSeries(tv);
        }

        [Test]
        public async Task GetSearchResultsAsync_ValidQueryButNoFallBack_ReturnsEmptyCollection()
        {
            var resourcesFolder = GetResourceFolder();
            var offlineDb = new OfflineDatabase(resourcesFolder, new ConsoleLogger())
            {
                FallbackProvider = null
            };

            var task = offlineDb.GetSearchResultsAsync("Friends", CancellationToken.None, new Progress<double>());

            var results = await task;

            Assert.IsFalse(task.IsFaulted);
            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
        }
    }
}