using UnityEngine;

namespace AI.Core.State.BehaviorContext
{
    public interface IPositionContext
    {
        // public Vector2 CurrentSpeed { get; }
        public Vector2 CurrentPosition { get; }
        // public Vector2 Destination { get; }
    }
}