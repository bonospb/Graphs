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
        public string FindShortestPath(T startValue, T finishValue) =>
            FindShortestPath(_graph.Find(startValue), _graph.Find(finishValue));

        public string FindShortestPath(Vertex<T> startVertex, Vertex<T> finishVertex)
        {
            if (FindShortestPath(startVertex.Value, finishVertex.Value, out var path))
                return string.Join(" -> ", path);

            return "Path not found!";
        }

        public bool FindShortestPath(T startValue, T finishValue, out T[] path)
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

            distance[startValue] = 0;

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

            path = GetPath(startValue, finishValue, parent).ToArray();

            return path.Last().Equals(finishValue);
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

        private IEnumerable<T> GetPath(T u, T v, Dictionary<T, T> parent)
        {
            LinkedList<T> path = new();
            path.AddFirst(v);
            while (!v.Equals(u))
            {
                v = parent[v];
                path.AddFirst(v);
            }

            return path;
        }
        #endregion
    }
}
