using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arachnee.TmdbProviders.Tests
{
    [TestFixture]
    public class OnlineDatabaseTests : TmdbDatabaseTests
    {
        [Test]
        public async Task GetEntryAsync_MovieId_ReturnsCorrectMovie()
        {
            var resourcesFolder = GetResourceFolder();
            var onlineDb = new OnlineDatabase(resourcesFolder, new ConsoleLogger());
            var task = onlineDb.GetEntryAsync(Id.FromMovieNumber(280), CancellationToken.None, new Progress<double>());
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
            var onlineDb = new OnlineDatabase(resourcesFolder, new ConsoleLogger());
            var task = onlineDb.GetEntryAsync(Id.FromArtistNumber(1100), CancellationToken.None, new Progress<double>());
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
            var onlineDb = new OnlineDatabase(resourcesFolder, new ConsoleLogger());
            var task = onlineDb.GetEntryAsync(Id.FromTvSeriesNumber(1668), CancellationToken.None, new Progress<double>());
            var entry = await task;

            Assert.IsFalse(task.IsFaulted);
            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            var tv = entry as TvSeries;

            AssertTvSeries(tv);
        }

        [Test]
        public async Task GetSearchResultsAsync_ValidQuery_ReturnsCorrectResults()
        {
            var resourcesFolder = GetResourceFolder();
            var onlineDb = new OnlineDatabase(resourcesFolder, new ConsoleLogger());
            var task = onlineDb.GetSearchResultsAsync("Jackie Chan", CancellationToken.None, new Progress<double>());

            var results = await task;

            Assert.IsFalse(task.IsFaulted);
            Assert.IsTrue(results.Count > 10);

            var movieResult = results.FirstOrDefault(r => r.Name == "First Strike");
            var personResult = results.FirstOrDefault(r => r.Name == "Jackie Chan");
            var tvResult = results.FirstOrDefault(r => r.Name == "Jackie Chan Adventures");

            Assert.IsNotNull(movieResult);
            Assert.IsNotNull(personResult);
            Assert.IsNotNull(tvResult);

            Assert.AreEqual("Movie-9404", movieResult.EntryId);
            Assert.AreEqual("Artist-18897", personResult.EntryId);
            Assert.AreEqual("Serie-240", tvResult.EntryId);

            Assert.IsFalse(string.IsNullOrEmpty(movieResult.ImagePath));
            Assert.IsFalse(string.IsNullOrEmpty(personResult.ImagePath));
            Assert.IsFalse(string.IsNullOrEmpty(tvResult.ImagePath));
        }
    }
}