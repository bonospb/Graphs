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
        public void BellmanFord(T start)
        {
            var edges = _graph.Vertices.SelectMany(x => x.Edges.Select(y => new GraphEdgeInfo<T>(x, y.ConnectedNode, y.Weight)));

            int verticesCount = _graph.Count;
            int edgesCount = edges.Count();

            Dictionary<T, int> distance = new(verticesCount);

            foreach (var vertex in _graph.Vertices)
                distance.Add(vertex.Value, int.MaxValue);

            distance[start] = 0;

            for (int i = 1; i < verticesCount; ++i)
            {
                foreach (var edge in edges)
                {
                    T u = edge.Source.Value;
                    T v = edge.Destination.Value;
                    int weight = edge.Weight;

                    if (distance[u] != int.MaxValue && distance[u] + weight < distance[v])
                        distance[v] = distance[u] + weight;
                }
            }

            foreach (var edge in edges)
            {
                T u = edge.Source.Value;
                T v = edge.Destination.Value;
                int weight = edge.Weight;

                if (distance[u] != int.MaxValue && distance[u] + weight < distance[v])
                    Debug.Log("Graph contains negative weight cycle.");
            }

            var str = "Results\n";
            foreach (var d in distance)
                str += $"Vertex: {d.Key}, Distance: {d.Value}\n";

            Debug.Log(str);
        }
        #endregion

        #region Private methods
        private IEnumerable<T> GetPath()
        {
            var path = new LinkedList<T>();
            return path;
        }
        #endregion
    }
}
