using System;
using System.Collections.Generic;

namespace Arachnee.InnerCore.GraphBases
{
    public class UndirectedUnweightedGraphAlgorithms<TVertex>
    {
        private readonly UndirectedUnweightedGraph<TVertex> _graph;

        private readonly Dictionary<TVertex, Func<TVertex, ICollection<TVertex>>> _cachedFunctions = new Dictionary<TVertex, Func<TVertex, ICollection<TVertex>>>();

        public UndirectedUnweightedGraphAlgorithms(UndirectedUnweightedGraph<TVertex> graph)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }

        /// <summary>
        /// Returns all vertices reachable from the given vertex. 
        /// An optional Action can be executed when a reachable vertex is found.
        /// </summary>
        public ICollection<TVertex> BreadthFirstSearch(TVertex sourceVertex, Action<TVertex> discoverAccessibleFunc = null)
        {
            var accessibleVertices = new HashSet<TVertex>();

            if (!_graph.ContainsVertex(sourceVertex))
            {
                return accessibleVertices;
            }

            var queue = new Queue<TVertex>();
            queue.Enqueue(sourceVertex);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();

                if (accessibleVertices.Contains(vertex))
                {
                    continue;
                }

                discoverAccessibleFunc?.Invoke(vertex);

                accessibleVertices.Add(vertex);

                foreach (var child in _graph.GetSuccessors(vertex))
                {
                    if (!accessibleVertices.Contains(child))
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            return accessibleVertices;
        }

        /// <summary>
        /// Returns a Func able to return the shortest path from the given source vertex to any target vertex. 
        /// The resulting path doesn't contain the source vertex itself, but include the target. 
        /// Hence, an empty path means the target is unreachable from the source.
        /// </summary>
        public Func<TVertex, ICollection<TVertex>> ComputeShortestPathFunc(TVertex sourceVertex)
        {
            if (_cachedFunctions.ContainsKey(sourceVertex))
            {
                return _cachedFunctions[sourceVertex];
            }

            var parents = new Dictionary<TVertex, TVertex>();

            var queue = new Queue<TVertex>();
            queue.Enqueue(sourceVertex);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var child in _graph.GetSuccessors(vertex))
                {
                    if (parents.ContainsKey(child))
                    {
                        continue;
                    }

                    parents[child] = vertex;
                    queue.Enqueue(child);
                }
            }

            Func<TVertex, ICollection<TVertex>> shortestPathFunc = targetVertex =>
            {
                var path = new List<TVertex>();
                if (!parents.ContainsKey(targetVertex))
                {
                    // target is unreachable from source
                    return path;
                }

                var currentVertex = targetVertex;
                while (!currentVertex.Equals(sourceVertex))
                {
                    path.Add(currentVertex);
                    currentVertex = parents[currentVertex];
                }
                
                path.Reverse();

                return path;
            };

            _cachedFunctions[sourceVertex] = shortestPathFunc;

            return shortestPathFunc;
        }
    }
}