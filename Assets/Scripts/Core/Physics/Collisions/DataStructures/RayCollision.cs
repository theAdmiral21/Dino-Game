using UnityEngine;
using Physics.Core.PhysicsActors;

namespace Core.Physics.Collisions.DataStructures
{
    public struct RayCollision
    {
        public Vector2 CollisionPoint;
        public Vector2 Normal;
        public IPhysicsActor OtherActor;
    }
}