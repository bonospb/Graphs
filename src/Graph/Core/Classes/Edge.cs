namespace FreeTeam.Graph
{
    public class Edge<T>
    {
        #region Public
        public Vertex<T> ConnectedNode { get; }

        public int EdgeWeight { get; }
        #endregion

        public Edge(Vertex<T> connectedVertex, int weight)
        {
            ConnectedNode = connectedVertex;
            EdgeWeight = weight;
        }
    }
}
