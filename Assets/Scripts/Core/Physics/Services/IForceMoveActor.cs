using UnityEngine;
using Physics.Core.PhysicsActors;

namespace Physics.Core.Services
{
    public interface IForceMoveActor
    {
        public void ForceMoveTo(IPhysicsActor target, Vector2 destination);
    }
}