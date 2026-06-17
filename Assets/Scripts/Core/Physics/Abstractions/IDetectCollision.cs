using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Core.Physics.Collisions.DataStructures;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsActors;
using UnityEngine;

namespace Physics.Core.Abstractions
{
    public interface IDetectCollision
    {
        public HashSet<CollidingPair> CurrentCollisions { get; }

        public Dictionary<IPhysicsActor, Vector2> GetCollisions(List<IPhysicsActor> actors);
        public Dictionary<IPhysicsActor, Vector2> ResolveCollisions();
    }
}