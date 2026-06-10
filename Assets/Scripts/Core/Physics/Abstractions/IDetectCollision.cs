using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsActors;
using UnityEngine;

namespace Physics.Core.Abstractions
{
    public interface IDetectCollision
    {
        public HashSet<CollidingPair> Collisions { get; }

        public Dictionary<IPhysicsActor, Vector2> GetCollisions(List<IPhysicsActor> actors);
        public Dictionary<IPhysicsActor, Vector2> ResolveCollisions();
    }
}