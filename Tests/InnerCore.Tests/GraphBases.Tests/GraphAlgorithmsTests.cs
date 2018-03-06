using Arachnee.InnerCore.GraphBases;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arachnee.InnerCore.Tests.GraphBases.Tests
{
    [TestFixture]
    public class GraphAlgorithmsTests
    {
        [Test]
        public void GetShortestPath_ShortestPathExist_CorrectPath()
        {
            var graph = new UndirectedUnweightedGraph<int>();
            graph.AddVerticesAndEdgeRange(new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(2, 3),
                new Tuple<int, int>(3, 4)
            });

            int fromVertex = 1;
            int toVertex = 4;

            var algo = new UndirectedUnweightedGraphAlgorithms<int>(graph);
            var func = algo.ComputeShortestPathFunc(fromVertex);
            
            var res = func.Invoke(toVertex).ToList();

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(2, res[0]);
            Assert.AreEqual(3, res[1]);
            Assert.AreEqual(4, res[2]);
        }

        [Test]
        public void GetShortestPath_ShortestPathToVertexItself_EmptyPath()
        {
            var graph = new UndirectedUnweightedGraph<int>();
            graph.AddVertex(0);

            int fromVertex = 0;
            int toVertex = 0;

            var algo = new UndirectedUnweightedGraphAlgorithms<int>(graph);
            var func = algo.ComputeShortestPathFunc(fromVertex);

            var res = func.Invoke(toVertex).ToList();
            
            Assert.AreEqual(0, res.Count);
        }

        [Test]
        public void GetShortestPath_DisconnectedVertices_EmptyPath()
        {
            var graph = new UndirectedUnweightedGraph<int>();
            graph.AddVertex(0);
            graph.AddVertex(1);

            int fromVertex = 0;
            int toVertex = 1;

            var algo = new UndirectedUnweightedGraphAlgorithms<int>(graph);
            var func = algo.ComputeShortestPathFunc(fromVertex);

            var res = func.Invoke(toVertex).ToList();

            Assert.AreEqual(0, res.Count);
        }
        
        [Test]
        public void GetShortestPath_VerticesDontExist_EmptyPath()
        {
            var graph = new UndirectedUnweightedGraph<int>();
            graph.AddVertex(0);
            graph.AddVertex(1);

            int fromVertex = 2;
            int toVertex = 4;

            var algo = new UndirectedUnweightedGraphAlgorithms<int>(graph);
            var func = algo.ComputeShortestPathFunc(fromVertex);

            var res = func.Invoke(toVertex).ToList();

            Assert.AreEqual(0, res.Count);
        }
    }
}