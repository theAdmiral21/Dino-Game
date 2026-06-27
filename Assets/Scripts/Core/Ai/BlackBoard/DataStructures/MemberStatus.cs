using Primitives.Health;
using UnityEngine;

namespace Core.Ai.BlackBoard
{
    public class MemberStatus
    {
        public HealthState Health;
        public Vector2 Position;
        public Status CurrentStatus;
        public AlertLevel Alertness;
    }
}