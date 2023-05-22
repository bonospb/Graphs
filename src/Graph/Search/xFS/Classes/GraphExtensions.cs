using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeTeam.Graph.Search
{
    public static class GraphExtensions
    {
        public static string Search<T>(this Graph<T> graph, T start, T finish, GraphSearchTypes searchType)
        {
            LinkedList<Vertex<T>> searchList = new();
            if (start.Equals(finish))
            {
                return start.ToString();
            }
            else if (graph.Find(start) == null || graph.Find(finish) == null)
            {
                return string.Empty;
            }
            else
            {
                var startNode = graph.Find(start);
                Dictionary<Vertex<T>, Vertex<T>> pathNodes = new();
                pathNodes.Add(startNode, null);

                searchList.AddFirst(startNode);

                while (searchList.Count > 0)
                {
                    var currentNode = searchList.First.Value;
                    searchList.RemoveFirst();

                    foreach (var edge in currentNode.Edges)
                    {
                        if (edge.ConnectedNode.Value.Equals(finish))
                        {
                            pathNodes.Add(edge.ConnectedNode, currentNode);
                            return "Final Path: " + string.Join(" -> ", ConvertToPath(edge.ConnectedNode, pathNodes));
                        }
                        else if (pathNodes.ContainsKey(edge.ConnectedNode))
                        {
                            continue;
                        }
                        else
                        {
                            pathNodes.Add(edge.ConnectedNode, currentNode);

                            if (searchType == GraphSearchTypes.DFS)
                                searchList.AddFirst(edge.ConnectedNode);
                            else
                                searchList.AddLast(edge.ConnectedNode);
                        }
                    }
                }

                return "Path not found!";
            }
        }

        public static bool Search<T>(this Graph<T> graph, T start, T finish, GraphSearchTypes searchType, out List<T> path, IEnumerable<T> exclude = null)
        {
            path = new();

            LinkedList<Vertex<T>> searchList = new();
            if (start.Equals(finish))
            {
                path.Add(start);
                return true;
            }
            else if (graph.Find(start) == null || graph.Find(finish) == null)
            {
                return false;
            }
            else
            {
                var startNode = graph.Find(start);
                Dictionary<Vertex<T>, Vertex<T>> pathNodes = new();
                pathNodes.Add(startNode, null);

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

                            path = ConvertToPath(edge.ConnectedNode, pathNodes).Select(x => x.Value).ToList();

                            return true;
                        }
                        else if (pathNodes.ContainsKey(edge.ConnectedNode))
                        {
                            continue;
                        }
                        else
                        {
                            pathNodes.Add(edge.ConnectedNode, currentNode);

                            if (searchType == GraphSearchTypes.DFS)
                                searchList.AddFirst(edge.ConnectedNode);
                            else
                                searchList.AddLast(edge.ConnectedNode);
                        }
                    }
                }

                return false;
            }
        }

        public static bool SearchAny<T>(this Graph<T> graph, T start, IEnumerable<T> finish, GraphSearchTypes searchType, IEnumerable<T> exclude = null)
        {
            foreach (var f in finish)
            {
                if (graph.Search(start, f, searchType, out var _, exclude))
                    return true;
            }

            return false;
        }

        public static bool SearchAll<T>(this Graph<T> graph, T start, IEnumerable<T> finish, GraphSearchTypes searchType, IEnumerable<T> exclude = null)
        {
            foreach (var f in finish)
            {
                if (!graph.Search(start, f, searchType, out var _, exclude))
                    return false;
            }

            return true;
        }

        private static LinkedList<Vertex<T>> ConvertToPath<T>(Vertex<T> endNode, Dictionary<Vertex<T>, Vertex<T>> pathNodes)
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
    }
}
