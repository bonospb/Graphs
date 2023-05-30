using Mono.Cecil.Cil;
using System;

namespace FreeTeam.Graph.Search
{
    public class GraphEdgeInfo<T> : IEquatable<GraphEdgeInfo<T>>
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

        public bool Equals(GraphEdgeInfo<T> obj)
        {
            if (obj is null) 
                return false;

            if (Object.ReferenceEquals(this, obj)) 
                return true;

            return ((Source.Value.Equals(obj.Destination.Value)
            &&
            Destination.Value.Equals(obj.Source.Value))
            ||
            (Source.Value.Equals(obj.Source.Value)
            &&
            Destination.Value.Equals(obj.Destination.Value)))
            && Weight == obj.Weight;
        }

        public override int GetHashCode()
        {
            int hashSourceValue = Source.Value == null ? 0 : Source.Value.GetHashCode();
            int hashDestinationValue = Destination.Value == null ? 0 : Destination.Value.GetHashCode();
            int hashProductCode = Weight.GetHashCode();

            return hashSourceValue ^ hashDestinationValue ^ hashProductCode;
        }
    }
}
