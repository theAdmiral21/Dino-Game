using UnityEngine;

namespace Primitives.Physics
{
    [System.Serializable]
    public struct KinematicResult
    {
        public Vector2 CurrentPosition;
        public Vector2 Velocity;
        public Vector2 ExternalVelocity;
        public Vector2 Acceleration;
        public Vector2 CornerNudge;
        public float Angle;
        public float RotationalVelocity;
        public float RotationalAcceleration;
        public float Gravity;
        public float Dt;
        public Vector2 FrameDelta;
        public float AngularFrameDelta;

        // public void SetFrameDelta(Vector2 delta) => FrameDelta = delta;

        public void ClearForces()
        {
            Velocity = Vector2.zero;
            ExternalVelocity = Vector2.zero;
            Acceleration = Vector2.zero;
            Gravity = 0;
            Angle = 0;
            RotationalVelocity = 0;
        }
    }
}