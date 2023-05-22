using System.Collections.Generic;
using System.Text;

namespace FreeTeam.Graph
{
    public class Graph<T>
    {
        #region Public
        public int Count => vertices.Count;
        public IList<Vertex<T>> Vertices => vertices.AsReadOnly();
        #endregion

        #region Private
        private readonly List<Vertex<T>> vertices = new();
        #endregion

        #region Public methods        
        public bool AddVertex(T value)
        {
            if (Find(value) == null)
            {
                vertices.Add(new Vertex<T>(value));
                return true;
            }

            return false;
        }

        public bool[] AddVertices(T[] values)
        {
            var result = new bool[values.Length];

            for (var i = 0; i < result.Length; i++)
                result[i] = AddVertex(values[i]);

            return result;
        }

        public bool AddEdge(T value1, T value2, int weight = 0)
        {
            var vertex1 = Find(value1);
            var vertex2 = Find(value2);
            if (vertex1 != null && vertex2 != null)
            {
                vertex1.AddEdge(vertex2, weight);
                vertex2.AddEdge(vertex1, weight);

                return true;
            }

            return false;
        }

        public bool[] AddEdges(T value, T[] neighbors)
        {
            var result = new bool[neighbors.Length];
            for (var i = 0; i < neighbors.Length; i++)
                result[i] = AddEdge(neighbors[i], value);

            return result;
        }

        public Vertex<T> Find(T value) =>
             vertices.Find(x => x.Value.Equals(value));

        public void Clear()
        {
            foreach (var vertex in vertices)
                vertex.RemoveAllEdges();

            for (int i = vertices.Count - 1; i >= 0; i--)
                vertices.RemoveAt(i);
        }

        public override string ToString()
        {
            StringBuilder vertexString = new();
            vertexString.Append("Graph type: [");
            vertexString.Append(typeof(T).Name);
            vertexString.Append("]\n");

            for (int i = 0; i < Count; i++)
            {
                vertexString.Append(vertices[i].ToString());
                if (i < Count - 1)
                    vertexString.Append("\n");
            }

            return vertexString.ToString();
        }
        #endregion
    }
}