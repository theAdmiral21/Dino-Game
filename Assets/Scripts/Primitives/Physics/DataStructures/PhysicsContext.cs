using UnityEngine;
using Primitives.Audio;
using Environment.Platforms.Primitives;

namespace Primitives.Physics
{
    [System.Serializable]
    public class PhysicsContext // Consider breaking this up using interfaces, like IMoveable for platform shenanigans. Or IPlayerState for states only the player can enter?
    {
        public bool IsGrounded;
        public bool IsRising;
        public bool IsFalling;
        public bool IsOnPlatform;
        public bool IsTouchingWall;
        public WallContact WallContactType;
        public bool IsWallSliding;
        public bool IsHanging;
        public bool IsFloating;
        public bool IsSliding;
        public bool Heading;
        public Vector2 Velocity => _velocity;
        private Vector2 _velocity;
        public ContactType FloorContactType;
        public SurfaceType Surface;

        public Vector3 DeltaPosition;
        public Vector3 PrevPos;

        public Vector3 GlobalPosition;

        public IMotionProvider MotionProvider;



        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        // public void SetPlatformDelta(Vector2 platformDelta)
        // {
        //     _platformDelta = platformDelta;
        // }

        // Debug stuff
        public string PlatformName;
    }
}
