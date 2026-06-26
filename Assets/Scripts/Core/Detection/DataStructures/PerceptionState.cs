using UnityEngine;
using Primitives.Health;

namespace Core.Detection.DataStructures
{
    public class PerceptionState
    {
        public float ConfidenceLevel;

        // Visual
        public Vector2? TargetPosition;
        public Vector2? TargetVelocity;
        public HealthState TargetHealth;
        public float TimeOfVisual;
        public float VisualIntensity;

        // Audio
        public Vector2? AudioDirection;
        public float TimeOfAudio;
        public float AudioIntensity;

        // Scent
        public Vector2? ScentDirection;
        public float TimeOfScent;
        public float ScentIntensity;
    }
}