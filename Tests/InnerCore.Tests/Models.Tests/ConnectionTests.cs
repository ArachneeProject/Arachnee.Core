using Arachnee.InnerCore.Models;
using NUnit.Framework;
using System.Linq;

namespace Arachnee.InnerCore.Tests.Models.Tests
{
    [TestFixture]
    public class ConnectionTests
    {
        [Test]
        public void AllTypes_ReturnsNonEmptyCollection()
        {
            Assert.IsTrue(Connection.AllTypes().Any());
        }
    }
}