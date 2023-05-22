using FreeTeam.Graph;

namespace FreeTeam.Graph.Search
{
    public class GraphVertexInfo<V>
    {
        #region Public
        public Vertex<V> Vertex { get; }
        public Vertex<V> PreviousNode { get; set; }

        public bool IsUnvisited { get; set; }

        public int EdgesWeightSum { get; set; }
        #endregion

        public GraphVertexInfo(Vertex<V> vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousNode = null;
        }
    }
}
