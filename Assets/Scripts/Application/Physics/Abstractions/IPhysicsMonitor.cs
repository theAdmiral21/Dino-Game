using Physics.Core.Abstractions;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsActors;
using Primitives.Physics;
using UnityEngine;

namespace Physics.Application.Abstractions
{
    public interface IPhysicsMonitor
    {
        public void UpdatePhysicsContext(IPhysicsActor actor, KinematicResult kinematicState, PhysicsContext context, RaycastConfiguration rayConfig);
        public void ObserveVelocity(Vector2 velocity);
    }
}