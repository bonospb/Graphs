using System.Collections.Generic;
using System.Text;

namespace FreeTeam.Graph
{
    public class Vertex<T>
    {
        #region Public
        public T Value => _value;
        public IList<Edge<T>> Edges => _edges.AsReadOnly();
        #endregion

        #region Private
        private readonly T _value;
        private readonly List<Edge<T>> _edges = new();
        #endregion

        public Vertex(T value) =>
            _value = value;

        #region Public methods
        public bool AddEdge(Edge<T> edge)
        {
            if (_edges.Contains(edge))
                return false;

            _edges.Add(edge);
            return true;
        }

        public bool AddEdge(Vertex<T> vertex, int weight) =>
            AddEdge(new Edge<T>(vertex, weight));

        public bool RemoveEdge(Edge<T> edge) =>
            _edges.Remove(edge);

        public void RemoveAllEdges()
        {
            for (int i = _edges.Count; i >= 0; i--)
            {
                _edges.RemoveAt(i);
            }
        }

        public override string ToString()
        {
            StringBuilder vertexString = new();
            vertexString.Append("Vertex: ");
            vertexString.Append(_value);
            vertexString.Append(" | With Edges [");

            for (int i = 0; i < _edges.Count; i++)
            {
                vertexString.Append(_edges[i]);
                if (i < _edges.Count - 1)
                    vertexString.Append(", ");
            }

            vertexString.Append("]");
            return vertexString.ToString();
        }
        #endregion
    }
}
