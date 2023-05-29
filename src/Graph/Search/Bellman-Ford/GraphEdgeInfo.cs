namespace FreeTeam.Graph.Search
{
    public class GraphEdgeInfo<T>
    {
        #region Public
        public Vertex<T> Source { get; }
        public Vertex<T> Destination { get; }
        public int Weight { get; }
        #endregion

        public GraphEdgeInfo(Vertex<T> source, Vertex<T> destination, int weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }
    }
}
