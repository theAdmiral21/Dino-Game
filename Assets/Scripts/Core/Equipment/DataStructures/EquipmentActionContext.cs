using System.Collections.Generic;
using Core.Movement.Inputs;
using Primitives.GameState;
using Primitives.Physics;

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