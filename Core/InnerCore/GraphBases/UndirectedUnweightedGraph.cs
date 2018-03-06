using System;
using System.Collections.Generic;
using System.Linq;

namespace Arachnee.InnerCore.GraphBases
{
    public class UndirectedUnweightedGraph<TVertex>
    {
        private readonly Dictionary<TVertex, HashSet<TVertex>> _successors = new Dictionary<TVertex, HashSet<TVertex>>();
        
        public int VertexCount => _successors.Keys.Count;
        public int EdgeCount => _successors.Values.Sum(adjacencyCollectionValue => adjacencyCollectionValue.Count) / 2;

        public IEnumerable<TVertex> Vertices => _successors.Keys;

        /// <summary>
        /// Add the given vertex to the graph.
        /// </summary>
        public virtual bool AddVertex(TVertex vertex)
        {
            var alreadyPresent = _successors.ContainsKey(vertex);
            if (alreadyPresent)
            {
                return false;
            }

            _successors[vertex] = new HashSet<TVertex>();
            return true;
        }

        /// <summary>
        /// Returns true if the graph contains the given vertex, false otherwise.
        /// </summary>
        public virtual bool ContainsVertex(TVertex vertex)
        {
            return _successors.Keys.Contains(vertex);
        }

        /// <summary>
        /// Add all the given vertices and edges (even if they don't exist)
        /// </summary>
        public virtual bool AddVerticesAndEdgeRange(ICollection<Tuple<TVertex, TVertex>> edges)
        {
            bool added = true;

            foreach (var edge in edges)
            {
                var source = edge.Item1;
                var target = edge.Item2;

                if (source.Equals(target))
                {
                    // ignore self edge
                    added = false;
                    continue;
                }

                if (!_successors.ContainsKey(source))
                {
                    _successors[source] = new HashSet<TVertex>();
                }

                if (!_successors.ContainsKey(target))
                {
                    _successors[target] = new HashSet<TVertex>();
                }

                added = added && _successors[source].Add(target);
                added = added && _successors[target].Add(source);
            }

            return added;
        }

        /// <summary>
        /// Returns true if the graph contains an edge between the two given vertices.
        /// </summary>
        public virtual bool ContainsEdge(TVertex sourceVertex, TVertex targetVertex)
        {
            return _successors.ContainsKey(sourceVertex)
                   && _successors[sourceVertex].Contains(targetVertex)
                   && _successors.ContainsKey(targetVertex)
                   && _successors[targetVertex].Contains(sourceVertex);
        }

        /// <summary>
        /// Returns the successors of the given vertex.
        /// </summary>
        public ICollection<TVertex> GetSuccessors(TVertex vertex)
        {
            if (vertex == null)
            {
                throw new ArgumentNullException(nameof(vertex));
            }

            if (ContainsVertex(vertex))
            {
                return _successors[vertex];
            }

            return new HashSet<TVertex>();
        }
        
        public ICollection<TVertex> GetPredecessors(TVertex vertex)
        {
            if (vertex == null)
            {
                throw new ArgumentNullException(nameof(vertex));
            }

            if (ContainsVertex(vertex))
            {
                return _successors[vertex];
            }

            return new HashSet<TVertex>();
        }
    }
}