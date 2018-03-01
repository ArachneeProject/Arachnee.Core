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
            var entry = new Movie("Movie-280");

            Assert.IsFalse(Entry.IsNullOrDefault(entry));
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