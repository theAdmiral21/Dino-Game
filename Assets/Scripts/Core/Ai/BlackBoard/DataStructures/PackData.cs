using System.Collections.Generic;
using Primitives.Detection;
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
        public Observation<Vector2>? TargetFacing; // for creeping up behind the player
        public Observation<HealthState> TargetHealth;
        public Observation<PlayerStatus> TargetStatus;
        public AlertLevel PackAlertLevel = AlertLevel.Unaware;
        public Dictionary<int, IPackMember> Members = new();
    }
}