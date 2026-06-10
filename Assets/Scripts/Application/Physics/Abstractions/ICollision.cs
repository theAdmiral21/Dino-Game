using UnityEngine;

namespace Physics.Application.Abstractions
{
    public interface ICollision
    {
        // public IPhysicsActor other { get; }
        public Vector2 RelativeVelocity { get; }
        public Vector2 ImpulseNormal { get; }
    }
}