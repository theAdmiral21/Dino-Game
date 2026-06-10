using UnityEngine;
using Physics.Application.Abstractions;
using Physics.Core.Abstractions;
using Physics.Core.Services;
using Physics.Core.PhysicsActors;

namespace Physics.Application.Services
{
    public class ForceMoveActor : IForceMoveActor
    {
        private IMoveActor _actorMover;
        public ForceMoveActor(IMoveActor moveActor)
        {
            _actorMover = moveActor;
        }
        public void ForceMoveTo(IPhysicsActor target, Vector2 destination)
        {
            _actorMover.SetPosition(target, destination);
        }

    }
}