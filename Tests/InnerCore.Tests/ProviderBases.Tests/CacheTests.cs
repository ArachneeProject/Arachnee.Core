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
        public void GetOrAdd_AddElementTwice_ReturnsFirstAdded()
        {
            var cache = new Cache<int, string>();
            var firstValue = cache.GetOrAdd(0, "zero");
            var secondValue = cache.GetOrAdd(0, "ZERO");
            
            Assert.AreEqual("zero", firstValue);
            Assert.AreEqual("zero", secondValue);
        }
    }
}