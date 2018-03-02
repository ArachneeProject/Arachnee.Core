using Arachnee.InnerCore.Models;
using NUnit.Framework;

namespace Arachnee.InnerCore.Tests.Models.Tests
{
    [TestFixture]
    public class EntryTests
    {
        [Test]
        public void IsNullOrDefault_ValidEntry_ReturnsFalse()
        {
            var movie = new Movie("Movie-280");
            var artist = new Artist("Artist-1100");
            var tv = new TvSeries("TvSeries-433");

            Assert.IsFalse(Entry.IsNullOrDefault(movie));
            Assert.IsFalse(Entry.IsNullOrDefault(artist));
            Assert.IsFalse(Entry.IsNullOrDefault(tv));
        }

        [Test]
        public void IsNullOrDefault_Null_ReturnsTrue()
        {
            Assert.IsTrue(Entry.IsNullOrDefault(null));
        }

        [Test]
        public void IsNullOrDefault_DefaultEntry_ReturnsTrue()
        {
            Assert.IsTrue(Entry.IsNullOrDefault(DefaultEntry.Instance));
        }
    }
}