using Core.Detection.DataStructures;
using Primitives.Health;
using UnityEngine;

namespace Core.Ai.BlackBoard
{
    [System.Serializable]
    public class MemberStatus
    {
        public HealthState Health;
        public Vector2 Position;
        public Status CurrentStatus;
        public AlertLevel Alertness;
        public PerceptionState Perception;
    }
}