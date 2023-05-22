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

            return GetPath(startNode, finishNode);
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
            foreach (var i in _infos)
            {
                if (i.Vertex.Equals(v))
                    return i;
            }

            return null;
        }

        private void SetSumToNextNode(GraphVertexInfo<T> info)
        {
            info.IsUnvisited = false;
            foreach (var e in info.Vertex.Edges)
            {
                var nextInfo = GetNodeInfo(e.ConnectedNode);
                var sum = info.EdgesWeightSum + e.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousNode = info.Vertex;
                }
            }
        }

        private string GetPath(Vertex<T> startNode, Vertex<T> endNode)
        {
            var path = endNode.Value.ToString();
            while (startNode != endNode)
            {
                endNode = GetNodeInfo(endNode).PreviousNode;
                path = endNode.Value.ToString() + " -> " + path;
            }

            return path;
        }
        #endregion
    }
}
