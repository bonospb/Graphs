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
            if (FindShortestPath(startVertex, finishVertex, out var path))
                return string.Join(" -> ", path);

            return "Path not found!";
        }

        public bool FindShortestPath(Vertex<T> startValue, Vertex<T> finishValue, out T[] path)
        {
            var edges = _graph.Vertices
                .SelectMany(x => x.Edges.Select(y => new GraphEdgeInfo<T>(x, y.ConnectedNode, y.Weight)))
                .Distinct()
                .ToArray();

            int verticesCount = _graph.Count;
            int edgesCount = edges.Count();

            Dictionary<Vertex<T>, int> distance = new(verticesCount);
            Dictionary<Vertex<T>, Vertex<T>> parent = new(verticesCount);

            foreach (var vertex in _graph.Vertices)
            {
                distance.Add(vertex, int.MaxValue);
                parent.Add(vertex, null);
            }

            distance[startValue] = 0;

            for (int i = 1; i < verticesCount; ++i)
            {
                foreach (var edge in edges)
                    Relax(edge.Source, edge.Destination, edge.Weight, distance, parent);
            }

            foreach (var edge in edges)
            {
                var u = edge.Source;
                var v = edge.Destination;
                var weight = edge.Weight;

                if (distance[u] != int.MaxValue && distance[u] + weight < distance[v])
                    Debug.Log($"Graph contains negative weight cycle! Edge: {edge.Source.Value} <-> {edge.Destination.Value}, Weight: ({edge.Weight})");
            }

            path = GetPath(startValue, finishValue, parent).ToArray();

            return path.Last().Equals(finishValue.Value);
        }
        #endregion

        #region Private methods
        private void Relax(Vertex<T> u, Vertex<T> v, int weight, Dictionary<Vertex<T>, int> distance, Dictionary<Vertex<T>, Vertex<T>> parent)
        {
            if (distance[u] != int.MaxValue && distance[v] > distance[u] + weight)
            {
                distance[v] = distance[u] + weight;
                parent[v] = u;
            }
        }

        private IEnumerable<T> GetPath(Vertex<T> u, Vertex<T> v, Dictionary<Vertex<T>, Vertex<T>> parent)
        {
            LinkedList<T> path = new();
            path.AddFirst(v.Value);
            while (!v.Equals(u))
            {
                v = parent[v];
                path.AddFirst(v.Value);
            }

            return path;
        }
        #endregion
    }
}
