using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using Arachnee.TmdbProvider.Online;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Arachnee.TmdbProvider.Tests
{
    [TestFixture]
    public class OnlineDatabaseTests
    {
        [Test]
        public async Task GetEntryAsync_MovieId_ReturnsCorrectMovie()
        {
            var onlineDb = new OnlineDatabase(new ConsoleLogger());
            var task = onlineDb.GetEntryAsync(Id.FromMovieNumber(280), new CancellationToken(), new Progress<double>());
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
            var onlineDb = new OnlineDatabase(new ConsoleLogger());
            var task = onlineDb.GetEntryAsync(Id.FromArtistNumber(1100), new CancellationToken(), new Progress<double>());
            var entry = await task;

            Assert.IsFalse(task.IsFaulted);
            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            var artist = entry as Artist;
            Assert.IsNotNull(artist);
            Assert.AreEqual("Arnold Schwarzenegger", artist.Name);
        }

        [Test]
        public async Task GetEntryAsync_TvSeriesId_ReturnsCorrectTvSeries()
        {
            var onlineDb = new OnlineDatabase(new ConsoleLogger());
            var task = onlineDb.GetEntryAsync(Id.FromTvSeriesNumber(1668), new CancellationToken(), new Progress<double>());
            var entry = await task;

            Assert.IsFalse(task.IsFaulted);
            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            var tv = entry as TvSeries;
            Assert.IsNotNull(tv);
            Assert.AreEqual("Friends", tv.Name);
        }
    }
}