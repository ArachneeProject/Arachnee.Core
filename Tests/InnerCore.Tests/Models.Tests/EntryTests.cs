using Arachnee.InnerCore.Models;
using NUnit.Framework;

namespace Arachnee.InnerCore.Tests.Models.Tests
{
    [TestFixture]
    public class EntryTests
    {
        private readonly Movie _movie = new Movie("Movie-280") { Title = "Terminator 2: Judgment Day"};
        private readonly Artist _artist = new Artist("Artist-1100");
        private readonly TvSeries _tv = new TvSeries("TvSeries-433");

        [Test]
        public void IsNullOrDefault_ValidEntry_ReturnsFalse()
        {
            Assert.IsFalse(Entry.IsNullOrDefault(_movie));
            Assert.IsFalse(Entry.IsNullOrDefault(_artist));
            Assert.IsFalse(Entry.IsNullOrDefault(_tv));
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