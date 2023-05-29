using System.Text;

namespace FreeTeam.Graph
{
    public class Edge<T>
    {
        #region Public
        public Vertex<T> ConnectedNode { get; }

        public int Weight { get; }
        #endregion

        public Edge(Vertex<T> connectedVertex, int weight)
        {
            ConnectedNode = connectedVertex;
            Weight = weight;
        }

        #region Public methods
        public override string ToString()
        {
            StringBuilder edgeString = new();
            edgeString.Append(ConnectedNode.Value);
            edgeString.Append("(");
            edgeString.Append(Weight);
            edgeString.Append(")");

            return edgeString.ToString();
        }
        #endregion
    }
}
