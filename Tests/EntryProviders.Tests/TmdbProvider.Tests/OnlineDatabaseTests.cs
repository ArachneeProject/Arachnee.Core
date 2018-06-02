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
        public async Task Test()
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
    }
}