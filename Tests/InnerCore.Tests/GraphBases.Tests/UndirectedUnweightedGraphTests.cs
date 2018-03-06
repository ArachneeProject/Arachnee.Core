using Arachnee.InnerCore.GraphBases;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Arachnee.InnerCore.Tests.GraphBases.Tests
{
    [TestFixture]
    public class UndirectedUnweightedGraphTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void ContainsVertex_VertexExist_ReturnsTrue()
        {
            var graph = new UndirectedUnweightedGraph<int>();
            graph.AddVertex(1);

            Assert.IsTrue(graph.ContainsVertex(1));
        }

        [Test]
        public void ContainsVertex_VertexDoesntExist_ReturnsFalse()
        {
            var graph = new UndirectedUnweightedGraph<int>();

            Assert.IsFalse(graph.ContainsVertex(1));
        }

        [Test]
        public void GetSuccessors_VertexExist_ReturnsChildren()
        {
            var graph = new UndirectedUnweightedGraph<int>();
            graph.AddVerticesAndEdgeRange(new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(1, 3)
            });

            var res = graph.GetSuccessors(1);

            Assert.AreEqual(2, res.Count);
            Assert.IsTrue(res.Contains(2));
            Assert.IsTrue(res.Contains(3));
        }

        [Test]
        public void AddVerticesAndEdge_ValidEdge_ReturnsTrue()
        {
            var graph = new UndirectedUnweightedGraph<string>();

            var edge = new Tuple<string, string>("Arnold Schwarzenegger", "The Terminator");

            var added = graph.AddVerticesAndEdgeRange(new[] { edge });

            Assert.IsTrue(added);
        }

        [Test]
        public void AddVerticesAndEdge_SelfEdge_ReturnsFalse()
        {
            var graph = new UndirectedUnweightedGraph<string>();

            var edge = new Tuple<string, string>("Arnold Schwarzenegger", "Arnold Schwarzenegger");

            var added = graph.AddVerticesAndEdgeRange(new[] { edge });

            Assert.IsFalse(added);
        }

        [Test]
        public void AddVerticesAndEdge_TwiceTheSameEdgeInstance_ReturnsFalse()
        {
            var graph = new UndirectedUnweightedGraph<string>();

            var edge = new Tuple<string, string>("Arnold Schwarzenegger", "The Terminator");

            graph.AddVerticesAndEdgeRange(new[] { edge });
            var added = graph.AddVerticesAndEdgeRange(new[] { edge });

            Assert.IsFalse(added);
        }

        [Test]
        public void AddVerticesAndEdge_TwoEdgesWithSameData_ReturnsFalse()
        {
            var graph = new UndirectedUnweightedGraph<string>();

            var edge1 = new Tuple<string, string>("Arnold Schwarzenegger", "The Terminator");
            var edge2 = new Tuple<string, string>("Arnold Schwarzenegger", "The Terminator");

            graph.AddVerticesAndEdgeRange(new[] { edge1 });
            var added = graph.AddVerticesAndEdgeRange(new[] { edge2 });

            Assert.IsFalse(added);
        }


    }
}