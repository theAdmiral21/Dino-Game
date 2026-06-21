using Physics.Core.PhysicsActors;
using Primitives.Audio;
using UnityEngine;

namespace Physics.Core.DataStructures
{
    public struct CollisionInfo
    {
        public Vector2 CollisionPoint;
        public Vector2 Normal;
        public Collider2D Collider;
        public IPhysicsActor OtherActor;
        public SurfaceType Surface;
    }
}