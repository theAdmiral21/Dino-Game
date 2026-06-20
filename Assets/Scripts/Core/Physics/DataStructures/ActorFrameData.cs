using System.Collections.Generic;
using Core.Physics.Collisions.DataStructures;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Stats;
using Physics.Core.PhysicsActors;
using Primitives.Physics;

namespace Physics.Core.DataStructures
{
    [System.Serializable]
    public class ActorFrameData
    {
        public string DebugName;
        public List<IActionResult> Results;
        public PhysicsContext PhysicsContext;
        public KinematicResult CurrentState;
        public IStatCollection ActorStats;
        public RaycastConfiguration RaycastConfig;
        public List<RayCollision> RayCollisions;

        public void ClearData()
        {
            CurrentState.ClearForces();
            Results.Clear();
            // This might be overkill?
            PhysicsContext.SetVelocity(CurrentState.Velocity);
            RayCollisions.Clear();
        }
    }

}
