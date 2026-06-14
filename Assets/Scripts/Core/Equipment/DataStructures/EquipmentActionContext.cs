using System.Collections.Generic;
using Primitives.GameState;
using Primitives.Physics;
using Movement.Core.Inputs;

namespace Core.Equipment.DataStructures
{
    [System.Serializable]
    public class EquipmentActionContext
    {
        public List<IEquipmentActionRequest> CurrentRequests;
        public PhysicsContext Facts;
        public object RuleState;
        public GameState CurrentGameState;
        public IActorInput InputValues;
        public float Dt;
    }
}