using Movement.Core.Enums;
using UnityEngine;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// Interface that contracts movement with an entity.
    /// </summary>
    public interface IMoveToContext
    {
        public MovementType MoveType { get; }
        public Vector2 Destination { get; }
        public Vector2 CurrentSpeed { get; }
        public Vector2 CurrentPosition { get; }
        public void SetDestination(Vector2 dest);
        public Vector2 GetNextWayPoint();
        public void Stop();
        public void MoveTo();
    };
}