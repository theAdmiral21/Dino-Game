using System.Numerics;

namespace AI.Core.State.Abstractions
{
    public interface IDestinationContext
    {
        public Vector2 Destination { get; }
    }
}