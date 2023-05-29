using System.Collections.Generic;

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

        public string FindShortestPath(Vertex<T> startNode, Vertex<T> finishNode)
        {
            InitInfo();

            var first = GetNodeInfo(startNode);
            first.EdgesWeightSum = 0;

            while (true)
            {
                var current = FindUnvisitedNodeWithMinSum();
                if (current == null)
                    break;

                SetSumToNextNode(current);
            }

            return string.Join(" -> ", GetPath(startNode, finishNode));
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

        private IEnumerable<T> GetPath(Vertex<T> startNode, Vertex<T> endNode)
        {
            var path = new LinkedList<T>();
            path.AddFirst(endNode.Value);
            while (startNode != endNode)
            {
                endNode = GetNodeInfo(endNode).PreviousNode;
                path.AddFirst(endNode.Value);
            }

            return path;
        }
        #endregion
    }
}
