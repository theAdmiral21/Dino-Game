using System.Collections.Generic;
using Physics.Core.PhysicsActors;
using UnityEngine;

namespace Physics.Core.DataStructures
{
    public struct MovementResolution
    {
        public Vector2 FrameDelta;
        public Vector2 CornerNudge;
        public bool GotCollision;
        public List<IPhysicsActor> CollidingActors;
    }
}