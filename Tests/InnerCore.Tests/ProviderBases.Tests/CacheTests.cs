using Arachnee.InnerCore.ProviderBases;
using NUnit.Framework;

namespace Arachnee.InnerCore.Tests.ProviderBases.Tests
{
    [TestFixture]
    public class CacheTests
    {
        [Test]
        public void TryGet_EmptyCache_ReturnsFalse()
        {
            var cache = new Cache<int, string>();

            string value;
            Assert.IsFalse(cache.TryGetValue(0, out value));
        }

        [Test]
        public void TryAdd_AddElementTwice_ReturnsLastAdded()
        {
            var cache = new Cache<int, string>();
            cache.AddOrUpdate(0, "zero");
            cache.AddOrUpdate(0, "ZERO");

            string value;
            Assert.IsTrue(cache.TryGetValue(0, out value));
            Assert.AreEqual("ZERO", value);
        }
    }
}