using System.Collections.Generic;
using Environment.Platforms.Primitives;
using Game.Core.Effects;
using Movement.Core.Abstractions;
using Physics.Application.Abstractions;
using Physics.Core.Abstractions;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsActors;
using Physics.Unity.Movement;
using Primitives.Audio;
using Primitives.Physics;
using UnityEngine;

namespace Physics.Unity.Physics
{
    public class PhysicsMonitor : MonoBehaviour, IPhysicsMonitor
    {
        [SerializeField] private RaycastController _raycastController;
        [SerializeField] private float FallThresh;
        [SerializeField] private float RiseThresh;
        [SerializeField] private LayerMask _collisionMask;
        [SerializeField] private bool _drawDebug;

        public WallContact WallContactSide { get; private set; }
        public string ContactTag { get; private set; }
        public PhysicsContext CurrentContext { get; private set; }

        const float MIN_GROUND_CHECK = 0.1f;
        const float MIN_WALL_CHECK = 0.1f;

        public float TerrainAngle => _terrainAngle;
        private float _terrainAngle;

        private RaycastProbe _surfaceProbe;
        private Vector2 _velocity;
        private Vector3 _prevPosition;
        private Collider2D _platform;

        const int PLATFORM_GRACE_FRAMES = 5;
        private int _platformGraceCounter;

        const int GROUNDED_GRACE_FRAMES = 0;
        private int _groundedGraceCounter;

        private bool _isGroundedThisFrame;
        private bool _isOnPlatformThisFrame;
        private bool _isRisingThisFrame;
        private bool _isFallingThisFrame;
        private bool _isAirborneThisFrame;
        private bool _isTouchingWallThisFrame;

        private IMotionProvider _motionProvider;
        private IPhysicsActor _currentActor;

        private void Awake()
        {
            // playerCollider = GetComponent<BoxCollider2D>();
            _surfaceProbe = new RaycastProbe(_collisionMask);
            CurrentContext = new();
        }

        public void ObserveVelocity(Vector2 velocity)
        {
            _velocity = velocity;
            // Debug.Log($"Updated velocity: {_velocity}");
        }

        // This needs to update my players now.. not be updated by the players
        public void UpdatePhysicsContext(IPhysicsActor actor, KinematicResult kinematicState, PhysicsContext context, RaycastConfiguration rayConfig)
        {
            _currentActor = actor;

            _isGroundedThisFrame = CheckGrounded(rayConfig);
            _isOnPlatformThisFrame = CheckPlatformed(rayConfig);
            _isAirborneThisFrame = CheckAirborne();
            _isRisingThisFrame = CheckRising(kinematicState.Velocity);
            _isFallingThisFrame = CheckFalling(kinematicState.Velocity);
            _isTouchingWallThisFrame = CheckTouchingWall(rayConfig);

            context.IsGrounded = _isGroundedThisFrame;
            context.IsRising = _isRisingThisFrame;
            context.IsFalling = _isFallingThisFrame;
            context.IsOnPlatform = _isOnPlatformThisFrame;
            context.IsTouchingWall = _isTouchingWallThisFrame;
            context.WallContactType = WallContactSide;
            context.IsWallSliding = CheckWallSliding(rayConfig);
            context.IsHanging = false;
            context.IsFloating = false;
            context.IsSliding = false;
            context.FloorContactType = ContactType.NormalSurface;
            context.Surface = GetSurfaceType(rayConfig);
            context.DeltaPosition = actor.Brain.FrameData.CurrentState.FrameDelta;
            context.GlobalPosition = transform.position;

            context.SetVelocity(kinematicState.Velocity);

            if (_motionProvider != null)
            {

                context.MotionProvider = _motionProvider;
            }
            else
            {
                context.MotionProvider = null;
            }

            if (_motionProvider != null)
            {
                context.PlatformName = _motionProvider.DebugName;
            }
            else
            {
                context.PlatformName = "None";
            }

            GetExternalVelocityProviders(actor, rayConfig);

        }

        private void GetExternalVelocityProviders(IPhysicsActor actor, RaycastConfiguration rayConfig)
        {
            float yDelta = Mathf.Abs(_velocity.y) * Time.fixedDeltaTime;
            float groundCheck = MIN_GROUND_CHECK + yDelta;

            // Get collider
            List<Collider2D> colliders = _surfaceProbe.CastAllDown(groundCheck, rayConfig);
            if (colliders == null) return;

            // get velocity providers
            foreach (var collider in colliders)
            {

                var velProviders = collider.GetComponentsInChildren<IExternalVelocityProvider>();
                if (velProviders == null) continue;
                foreach (var provider in velProviders)
                {
                    actor.Brain.UpdateRequestList(provider.GetVelocity());
                }
            }
        }

        private bool CheckGrounded(RaycastConfiguration rayConfig)
        {
            return GroundedRaycast(rayConfig);
        }

        /// <summary>
        /// Method for verifying if the player is on the ground.
        /// </summary>
        private bool GroundedRaycast(RaycastConfiguration rayConfig)
        {
            float yDelta = Mathf.Abs(_velocity.y) * Time.fixedDeltaTime;
            float groundCheck = MIN_GROUND_CHECK + yDelta;
            Collider2D collider = _surfaceProbe.CastDown(groundCheck, rayConfig, Color.black, _drawDebug);
            if (collider != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for verifying if the player is on a platform.
        /// </summary>
        private bool CheckPlatformed(RaycastConfiguration rayConfig)
        {
            bool rayHit = OnPlatformRaycast(rayConfig);
            return rayHit;
            // bool overlapHit = OnPlatformOverlap(rayConfig);

            // if (!rayHit && !overlapHit)
            // {
            //     _motionProvider = null;
            // }

            // return rayHit || overlapHit;
        }

        private bool OnPlatformOverlap(RaycastConfiguration rayConfig)
        {
            Collider2D overlap = Physics2D.OverlapBox(
                rayConfig.Bounds.Center,
                rayConfig.Bounds.Size,
                0,
                _collisionMask
            );

            if (overlap != null && overlap.CompareTag("Platform"))
            {
                var actor = overlap.GetComponent<IActorProvider>();
                if (actor.Actor != _currentActor)
                {
                    _motionProvider = overlap.GetComponent<IMotionProvider>();
                    if (_motionProvider != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool OnPlatformRaycast(RaycastConfiguration rayConfig)
        {
            float platformOffset = 0f;
            float relativeVelocityY = _velocity.y;

            if (_motionProvider != null)
            {
                relativeVelocityY -= _motionProvider.Velocity.y;
                float deltaY = _motionProvider.DeltaPosition.y;

                platformOffset = Mathf.Abs(deltaY);
            }

            float groundCheck = MIN_GROUND_CHECK + (Mathf.Abs(relativeVelocityY) * Time.fixedDeltaTime) + platformOffset;

            Collider2D collider = _surfaceProbe.CastDown(groundCheck, rayConfig, Color.yellow, _drawDebug);
            if (collider != null && collider.CompareTag("Platform"))
            {
                if (collider != _platform)
                {
                    _platform = collider;
                    _motionProvider = collider.GetComponent<IMotionProvider>();
                }
                return true;
            }
            _platform = null;
            return false;
        }

        private SurfaceType GetSurfaceType(RaycastConfiguration rayConfig)
        {
            Collider2D above = _surfaceProbe.CastUp(MIN_GROUND_CHECK, rayConfig, Color.purple, _drawDebug);
            Collider2D below = _surfaceProbe.CastDown(MIN_GROUND_CHECK, rayConfig, Color.purple, _drawDebug);
            Collider2D right = _surfaceProbe.CastRight(MIN_WALL_CHECK, rayConfig, Color.purple, _drawDebug);
            Collider2D left = _surfaceProbe.CastLeft(MIN_WALL_CHECK, rayConfig, Color.purple, _drawDebug);

            ISurfaceTag surface;
            if (above)
            {
                surface = above.GetComponent<ISurfaceTag>();
            }
            else if (below)
            {
                surface = below.GetComponent<ISurfaceTag>();
            }
            else if (left)
            {
                surface = left.GetComponent<ISurfaceTag>();
            }
            else if (right)
            {
                surface = right.GetComponent<ISurfaceTag>();
            }
            else
            {
                return SurfaceType.None;
            }
            // This is for when the surface you're standing on doesn't have a tag
            if (surface == null)
            {
                return SurfaceType.None;
            }
            return surface.Tag;

        }

        /// <summary>
        /// Method for verifying if the player is wallsliding.
        /// </summary>
        private bool CheckTouchingWall(RaycastConfiguration rayConfig)
        {
            Collider2D colliderRight = _surfaceProbe.CastRight(MIN_WALL_CHECK, rayConfig, Color.pink, _drawDebug);

            Collider2D colliderLeft = _surfaceProbe.CastLeft(MIN_WALL_CHECK, rayConfig, Color.pink, _drawDebug);

            if (colliderRight != null)
            {
                WallContactSide = WallContact.Right;
                if (colliderRight.CompareTag("Platform"))
                {
                    if (colliderRight != _platform)
                    {
                        _platform = colliderRight;
                        _motionProvider = colliderRight.GetComponent<IMotionProvider>();
                    }
                }
                return true;
            }
            else if (colliderLeft != null)
            {
                WallContactSide = WallContact.Left;
                if (colliderLeft.CompareTag("Platform"))
                {
                    if (colliderLeft != _platform)
                    {
                        _platform = colliderLeft;
                        _motionProvider = colliderLeft.GetComponent<IMotionProvider>();
                    }
                }
                return true;
            }
            else
            {
                WallContactSide = WallContact.None;
                _platform = null; // Do I need this? 
                return false;
            }

        }


        private bool CheckWallSliding(RaycastConfiguration rayConfig)
        {
            return _isFallingThisFrame && CheckTouchingWall(rayConfig);
        }

        /// <summary>
        /// Method for verifying if the player is falling.
        /// </summary>
        private bool CheckFalling(Vector2 velocity)
        {
            if (_isAirborneThisFrame)
            {
                if (velocity.y <= FallThresh)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method for verifying if the player is hanging from something.
        /// </summary>
        // private bool CheckHanging()
        // {
        //     if (playerFSM.IsGrabbing)
        //     {
        //         return true;
        //     }
        //     return false;
        // }

        /// <summary>
        /// Method for verifying if the player is rising through the air.
        /// </summary>
        private bool CheckRising(Vector2 velocity)
        {
            if (_isAirborneThisFrame)
            {
                // Debug.Log($"Rising velocity: {velocity.y}");
                if (velocity.y > RiseThresh)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method for checking if the player is on any surface.
        /// </summary>
        /// <returns></returns>
        private bool CheckAirborne()
        {
            if (!_isGroundedThisFrame && !_isOnPlatformThisFrame)
            {
                // We're falling so we're definitely not on a platform
                _platform = null;
                _motionProvider = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for verifying if the player is floating.
        /// </summary>
        private bool CheckFloating()
        {
            if (!_isOnPlatformThisFrame && !_isGroundedThisFrame && !_isRisingThisFrame && !_isFallingThisFrame)
            {
                return true;
            }
            return false;
        }

        private bool CheckSliding()
        {
            if (ContactTag == "Slide" && _velocity.y < 0)
            {
                return true;
            }
            return false;
        }

        // public float GetTerrainAngle()
        // {
        //     // ray cast straight down
        //     float dist = 3f;
        //     RaycastHit2D hit = Physics2D.Raycast(_rBody.position, Vector2.down, dist);
        //     if (hit.collider != null)
        //     {
        //         _terrainAngle = Mathf.Atan2(hit.normal.y, hit.normal.x);
        //         if (_terrainAngle == 90f || _terrainAngle == -90f)
        //         {
        //             GroundType = GroundType.Flat;
        //             return TerrainAngle;
        //         }
        //         GroundType = GroundType.Slope;
        //         return TerrainAngle;
        //     }
        //     GroundType = GroundType.None;
        //     return -Mathf.Infinity;

        // }

        // public void OnCollisionEnter2D(Collision2D collision)
        // {
        //     if (collision.collider != null)
        //     {
        //         if (ContactTag != collision.collider.tag)
        //         {
        //             ContactTag = collision.collider.tag;
        //         }
        //     }
        // }

        // public void OnCollisionStay2D(Collision2D collision)
        // {
        //     if (collision.collider != null)
        //     {
        //         if (ContactTag != collision.collider.tag)
        //         {
        //             ContactTag = collision.collider.tag;
        //         }
        //     }
        // }

        // public void OnCollisionExit2D(Collision2D collision)
        // {
        //     if (collision.collider != null)
        //     {
        //         if (ContactTag != collision.collider.tag)
        //         {
        //             ContactTag = collision.collider.tag;
        //         }
        //     }
        // }
    }
}
