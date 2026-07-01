using Core.Detection.DataStructures;
using Primitives.Detection;
using Primitives.Health;
using UnityEngine;

namespace Core.Ai.WorldState
{
    public struct RaptorWorldState
    {
        public HealthState MyHealth;
        public HealthState TargetHealth;
        public PlayerStatus TargetStatus;
        public float TargetDistance;
        public float TargetFacing;
        public float NearestPackMate;
        public int ActivePackMates;
        public bool TargetAware;

        private bool CalcPlayerAware(PerceptionState perception, Vector2 raptorPosition)
        {
            if (!perception.TargetFacing.HasValue) return false;

            // Is the player facing toward the raptor?
            Vector2 toRaptor = (raptorPosition - perception.TargetPosition.Value).normalized;
            float dot = Vector2.Dot(perception.TargetFacing.Value, toRaptor);
            bool facingRaptor = dot > 0.5f; // within ~60 degrees

            bool isAiming = perception.TargetStatus == PlayerStatus.Aiming;
            bool isDistracted = perception.TargetStatus == PlayerStatus.Reloading ||
                                perception.TargetStatus == PlayerStatus.Interacting ||
                                perception.TargetStatus == PlayerStatus.Healing;

            if (isDistracted) return false;
            if (isAiming && facingRaptor) return true;
            return facingRaptor;
        }
    }
}