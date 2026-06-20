using System.Collections.Generic;
using Core.Physics.Collisions.DataStructures;
using Physics.Core.PhysicsActors;
using UnityEngine;

namespace Physics.Core.DataStructures
{
    public struct MovementResolution
    {
        public Vector2 FrameDelta;
        public Vector2 CornerNudge;
        public List<RayCollision> RayCollisions;
    }
}