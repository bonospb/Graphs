using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FreeTeam.Graph.Search
{
    public class BellmanFordAlgorithm<T>
    {
        #region Private
        private readonly Graph<T> _graph;

        //private LinkedList<GraphVertexInfo<T>> _infos;
        #endregion

        public BellmanFordAlgorithm(Graph<T> graph) =>
            _graph = graph;

        #region Public methods
        public void BellmanFord(T start, T finish)
        {
            var edges = _graph.Vertices
                .SelectMany(x => x.Edges.Select(y => new GraphEdgeInfo<T>(x, y.ConnectedNode, y.Weight)))
                .Distinct()
                .ToArray();

            int verticesCount = _graph.Count;
            int edgesCount = edges.Count();

            Dictionary<T, int> distance = new(verticesCount);
            Dictionary<T, T> parent = new(verticesCount);

            foreach (var vertex in _graph.Vertices)
            {
                distance.Add(vertex.Value, int.MaxValue);
                parent.Add(vertex.Value, default);
            }

            distance[start] = 0;

            for (int i = 1; i < verticesCount; ++i)
            {
                foreach (var edge in edges)
                    Relax(edge.Source.Value, edge.Destination.Value, edge.Weight, distance, parent);
            }

            foreach (var edge in edges)
            {
                T u = edge.Source.Value;
                T v = edge.Destination.Value;
                int weight = edge.Weight;

                if (distance[u] != int.MaxValue && distance[u] + weight < distance[v])
                    Debug.Log($"Graph contains negative weight cycle! Edge: {edge.Source.Value} <-> {edge.Destination.Value}, Weight: ({edge.Weight})");
            }

            PrintPath(start, finish, distance, parent);
        }
        #endregion

        #region Private methods
        private void Relax(T u, T v, int weight, Dictionary<T, int> distance, Dictionary<T, T> parent)
        {
            if (distance[u] != int.MaxValue && distance[v] > distance[u] + weight)
            {
                distance[v] = distance[u] + weight;
                parent[v] = u;
            }
        }

        private void PrintPath(T u, T v, Dictionary<T, int> distance, Dictionary<T, T> parent)
        {
            if (v.Equals(default) || u.Equals(default))
                return;

            if (!v.Equals(u))
                PrintPath(u, parent[v], distance, parent);

            Debug.Log($"Vertex {v} weight: {distance[v]}");
        }
        #endregion
    }
}
