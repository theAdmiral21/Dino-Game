using Primitives.Health;
using UnityEngine;

namespace Core.Detection.Visual.DataStructures
{
    public struct VisualData
    {
        public float DetectionTime;
        // How far away is the target as a percentage of the vision distance
        public float DistanceFraction;
        public Vector2 TargetPosition;
        // Is the target looking left or right?
        public float TargetFacing;
        public Vector2 TargetVelocity;
        public HealthState Health; // If you can be seen, someone can determine how you feel
    }
}