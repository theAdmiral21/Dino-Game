using System.Collections.Generic;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;
using Movement.Core.Inputs;

namespace Physics.Core.DataStructures
{
    [System.Serializable]
    public class ActorActionContext
    {
        public List<IActionRequest> CurrentRequests;
        public PhysicsContext Facts;
        public object RuleState;
        public GameState CurrentGameState;
        public IActorInput InputValues;
        public float Dt;
    }
}