using System.Collections.Generic;
using Primitives.Health;
using UnityEngine;

namespace Core.Ai.BlackBoard.DataStructures
{
    [System.Serializable]
    public class PackData
    {
        public Observation<Vector2>? LastKnownLocation;
        public Observation<Vector2>? BestGuessLocation;
        public float BestGuessConfidence = 0f;
        public Observation<float>? TargetFacing; // for creeping up behind the player
        public Observation<HealthState> TargetHealth;
        public AlertLevel PackAlertLevel = AlertLevel.Unaware;
        // Should I have another enum describing the player's status? ie Moving, Standing, Reloading, Healing, Interacting

        public Dictionary<int, IPackMember> Members = new();
    }
}