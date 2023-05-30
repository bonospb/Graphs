using System.Collections.Generic;
using System.Linq;

namespace FreeTeam.Graph.Search
{
    public class DijkstraAlgorithm<T>
    {
        #region Private
        private readonly Graph<T> _graph;

        private LinkedList<GraphVertexInfo<T>> _infos;
        #endregion

        public DijkstraAlgorithm(Graph<T> graph) =>
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

        public bool FindShortestPath(Vertex<T> startVertex, Vertex<T> finishVertex, out T[] path)
        {
            InitInfo();

            var first = GetNodeInfo(startVertex);
            first.EdgesWeightSum = 0;

            while (true)
            {
                var current = FindUnvisitedNodeWithMinSum();
                if (current == null)
                    break;

                SetSumToNextNode(current);
            }

            path = GetPath(startVertex, finishVertex).ToArray();

            return path.Last().Equals(finishVertex.Value);
        }

        public GraphVertexInfo<T> FindUnvisitedNodeWithMinSum()
        {
            var minValue = int.MaxValue;
            GraphVertexInfo<T> minVertexInfo = null;
            foreach (var nodeInfo in _infos)
            {
                if (nodeInfo.IsUnvisited && nodeInfo.EdgesWeightSum < minValue)
                {
                    minVertexInfo = nodeInfo;
                    minValue = nodeInfo.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }
        #endregion

        #region Private methods
        private void InitInfo()
        {
            _infos = new LinkedList<GraphVertexInfo<T>>();
            foreach (var v in _graph.Vertices)
            {
                _infos.AddLast(new GraphVertexInfo<T>(v));
            }
        }

        private GraphVertexInfo<T> GetNodeInfo(Vertex<T> v)
        {
            foreach (var info in _infos)
            {
                if (info.Vertex.Equals(v))
                    return info;
            }

            return null;
        }

        private void SetSumToNextNode(GraphVertexInfo<T> info)
        {
            info.IsUnvisited = false;
            foreach (var e in info.Vertex.Edges)
            {
                var nextInfo = GetNodeInfo(e.ConnectedNode);
                var sum = info.EdgesWeightSum + e.Weight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousNode = info.Vertex;
                }
            }
        }

        private IEnumerable<T> GetPath(Vertex<T> startVertex, Vertex<T> endVertex)
        {
            var path = new LinkedList<T>();
            path.AddFirst(endVertex.Value);
            while (startVertex != endVertex)
            {
                endVertex = GetNodeInfo(endVertex).PreviousNode;
                path.AddFirst(endVertex.Value);
            }

            return path;
        }
        #endregion
    }
}
