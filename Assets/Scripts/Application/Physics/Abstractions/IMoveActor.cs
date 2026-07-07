using Physics.Core.PhysicsActors;
using UnityEngine;

namespace Physics.Application.Abstractions
{
    public interface IMoveActor
    {
        public void MoveActor(IPhysicsActor actor, Vector2 velocity);
        public void RotateActor(IPhysicsActor actor, float omega);
        public void SetPosition(IPhysicsActor actor, Vector2 position);
        public void SetXPosition(IPhysicsActor actor, float xPosition);
        public void SetYPosition(IPhysicsActor actor, float yPosition);
    }
}