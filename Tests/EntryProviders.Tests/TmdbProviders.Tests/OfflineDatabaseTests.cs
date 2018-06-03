using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using Arachnee.TmdbProviders.Offline;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Arachnee.TmdbProviders.Tests
{
    [TestFixture]
    public class OfflineDatabaseTests
    {
        private string GetFolder()
        {
            var path = Path.Combine(Constants.ApplicationFolder, "OfflineDatabase");
            return path;
        }

        [Test]
        public async Task GetEntryAsync_MovieId_ReturnsCorrectMovie()
        {
            var offlineDb = new OfflineDatabase(GetFolder(), new ConsoleLogger());
            offlineDb.LoadMovies();
            var task = offlineDb.GetEntryAsync(Id.FromMovieNumber(280), new CancellationToken(), new Progress<double>());

            var entry = await task;

            Assert.IsFalse(task.IsFaulted);
            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            var movie = entry as Movie;
            Assert.IsNotNull(movie);
            Assert.AreEqual("Terminator 2: Judgment Day", movie.Title);
        }

        [Test]
        public async Task GetEntryAsync_ArtistId_ReturnsCorrectArtist()
        {
            var offlineDb = new OfflineDatabase(GetFolder(), new ConsoleLogger());
            offlineDb.LoadArtists();
            var task = offlineDb.GetEntryAsync(Id.FromArtistNumber(54882), new CancellationToken(), new Progress<double>());
            var entry = await task;

            Assert.IsFalse(task.IsFaulted);
            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            var artist = entry as Artist;
            Assert.IsNotNull(artist);
            Assert.AreEqual("Morena Baccarin", artist.Name);
        }

        [Test]
        public async Task GetEntryAsync_TvSeriesId_ReturnsCorrectTvSeries()
        {
            var offlineDb = new OfflineDatabase(GetFolder(), new ConsoleLogger());
            offlineDb.LoadTvSeries();
            var task = offlineDb.GetEntryAsync(Id.FromTvSeriesNumber(1408), new CancellationToken(), new Progress<double>());
            var entry = await task;

            Assert.IsFalse(task.IsFaulted);
            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            var tv = entry as TvSeries;
            Assert.IsNotNull(tv);
            Assert.AreEqual("House", tv.Name);
        }
    }
}