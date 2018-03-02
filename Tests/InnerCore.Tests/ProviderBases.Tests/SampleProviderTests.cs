using Arachnee.InnerCore.LoggerBases;
using Arachnee.InnerCore.Models;
using Arachnee.InnerCore.ProviderBases;
using NUnit.Framework;
using System;
using System.Threading;

namespace Arachnee.InnerCore.Tests.ProviderBases.Tests
{
    [TestFixture]
    public class SampleProviderTests
    {
        private const string MovieId = "Movie-280";

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

            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            Assert.AreEqual(MovieId, entry.Id);
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

        [Test]
        public void GetEntryAsync_ValidIdAndNullProgress_ReturnsValidEntry()
        {
            var provier = new SampleProvider { Logger = _logger };

            var entry = provier.GetEntryAsync(MovieId, null, CreateCancellationToken()).Result;

            Assert.IsFalse(Entry.IsNullOrDefault(entry));
            Assert.AreEqual(MovieId, entry.Id);
        }
    }
}