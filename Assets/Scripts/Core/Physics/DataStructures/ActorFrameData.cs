using System.Collections.Generic;
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
        public List<IPhysicsActor> CollidingActors;

        public void ClearData()
        {
            CurrentState.ClearForces();
            Results.Clear();
            // This might be overkill?
            PhysicsContext.SetVelocity(CurrentState.Velocity);
            CollidingActors.Clear();
        }
    }

}
