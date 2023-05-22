using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeTeam.Graph.Search
{
    public class SearchAlgorithm<T>
    {
        #region Private
        private readonly Graph<T> _graph;
        #endregion

        public SearchAlgorithm(Graph<T> graph) =>
            _graph = graph;

        #region Public methods
        public string Search(T start, T finish, GraphSearchTypes searchType, IEnumerable<T> exclude = null)
        {
            if (Search(start, finish, searchType, out var path, exclude))
                return string.Join(" -> ", path);

            return "Path not found!";
        }

        public bool Search(T start, T finish, GraphSearchTypes searchType, out T[] path, IEnumerable<T> exclude = null)
        {
            path = Array.Empty<T>();
            if (_graph.Find(start) == null || _graph.Find(finish) == null)
                return false;

            if (start.Equals(finish))
            {
                path = new T[] { start };
                return true;
            }

            var startNode = _graph.Find(start);

            Dictionary<Vertex<T>, Vertex<T>> pathNodes = new() { { startNode, null } };

            LinkedList<Vertex<T>> searchList = new();
            searchList.AddFirst(startNode);
            while (searchList.Count > 0)
            {
                var currentNode = searchList.First.Value;
                searchList.RemoveFirst();

                foreach (var edge in currentNode.Edges)
                {
                    if (exclude != null && exclude.Contains(edge.ConnectedNode.Value))
                        continue;

                    if (edge.ConnectedNode.Value.Equals(finish))
                    {
                        pathNodes.Add(edge.ConnectedNode, currentNode);
                        path = ConvertToPath(edge.ConnectedNode, pathNodes).Select(x => x.Value).ToArray();

                        return true;
                    }

                    if (pathNodes.ContainsKey(edge.ConnectedNode))
                        continue;

                    pathNodes.Add(edge.ConnectedNode, currentNode);

                    if (searchType == GraphSearchTypes.DFS)
                        searchList.AddFirst(edge.ConnectedNode);
                    else
                        searchList.AddLast(edge.ConnectedNode);
                }
            }

            return false;
        }

        public bool SearchAny(T start, IEnumerable<T> finish, GraphSearchTypes searchType, IEnumerable<T> exclude = null)
        {
            foreach (var f in finish)
            {
                if (Search(start, f, searchType, out var _, exclude))
                    return true;
            }

            return false;
        }

        public bool SearchAll(T start, IEnumerable<T> finish, GraphSearchTypes searchType, IEnumerable<T> exclude = null)
        {
            foreach (var f in finish)
            {
                if (!Search(start, f, searchType, out var _, exclude))
                    return false;
            }

            return true;
        }
        #endregion

        #region Private methods
        private LinkedList<Vertex<T>> ConvertToPath(Vertex<T> endNode, Dictionary<Vertex<T>, Vertex<T>> pathNodes)
        {
            LinkedList<Vertex<T>> path = new();
            path.AddFirst(endNode);
            var previous = pathNodes[endNode];
            while (previous != null)
            {
                path.AddFirst(previous);
                previous = pathNodes[previous];
            }

            return path;
        }
        #endregion
    }
}
