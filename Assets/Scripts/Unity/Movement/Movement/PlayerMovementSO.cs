using UnityEngine;
using System;
using Primitives.Stats;
/*
This file is a holder for all stats for a player
*/
namespace Movement.Unity.Stats
{
    [CreateAssetMenu(fileName = "PlayerMovementStats", menuName = "Player/PlayerMovementStats")]

    /// <summary>
    /// BasePlayerStats - A class for holding the base values for various player stats like run speed, acceleration, and jump force to name a few.
    /// </summary>
    [Serializable]
    public class MovementSO : ScriptableObject, IMovementStats
    {
        // Limits   
        //=========================================================================
        /// <summary>
        /// How quickly the player can move left or right on the ground.
        /// </summary>
        [SerializeField] private float _groundSpeedLimit = 30f;
        public float GroundSpeedLimit { get => _groundSpeedLimit; set => _groundSpeedLimit = value; }

        /// <summary>
        /// How quickly the player is allowed to fall.
        /// </summary>
        [SerializeField] private float fallSpeedLimit = 30f;
        public float FallSpeedLimit { get => fallSpeedLimit; set => fallSpeedLimit = value; }

        /// <summary>
        /// The base run speed of the player.
        /// </summary>
        [SerializeField] private float runSpeed = 15f;
        public float RunSpeed { get => runSpeed; set => runSpeed = value; }

        /// <summary>
        /// The base sprint speed of the player.
        /// </summary>
        [SerializeField] private float sprintSpeed = 20f;
        public float SprintSpeed { get => sprintSpeed; set => sprintSpeed = value; }

        /// <summary>
        /// The rate at which the player accelerates to the sprint speed.
        /// </summary>
        [SerializeField] private float sprintAccel = 10f;
        public float SprintAccel { get => sprintAccel; set => sprintAccel = value; }

        // Running
        //=========================================================================
        /// <summary>
        /// How quickly the player accelerates while running on the ground.
        /// </summary>
        [SerializeField] private float runAccel = 40f;
        public float RunAccel { get => runAccel; set => runAccel = value; }
        /// <summary>
        /// How quickly the player slows down while on the ground.
        /// </summary>
        [SerializeField] private float brakeAccel = 40f;
        public float BrakeAccel { get => brakeAccel; set => brakeAccel = value; }

        // Quick step and pounces
        //=========================================================================
        /// <summary>
        /// How far in world units the player will move when quick stepping.
        /// </summary>
        [SerializeField] private float quickStepDistance = 30f;
        public float QuickStepDistance { get => quickStepDistance; set => quickStepDistance = value; }
        /// <summary>
        /// How long it should take the player to cover the quick step distance
        /// </summary>
        [SerializeField] private float quickStepDuration = .1f;
        public float QuickStepDuration { get => quickStepDuration; set => quickStepDuration = value; }
        /// <summary>
        /// The cool down interval between quick steps
        /// </summary>
        [SerializeField] private float quickStepCoolDown = .1f;
        public float QuickStepCoolDown { get => quickStepCoolDown; set => quickStepCoolDown = value; }
        /// <summary>
        /// The cool down interval between quick steps
        /// </summary>
        [SerializeField] private float longJumpWindow = .3f;
        public float LongJumpWindow { get => longJumpWindow; set => longJumpWindow = value; }


        // Aerial movement
        //=========================================================================
        /// <summary>
        /// How quickly the player accelerates left and right while airborne.
        /// </summary>
        [SerializeField] private float aerialAccel = 30f;
        public float AerialAccel { get => aerialAccel; set => aerialAccel = value; }
        /// <summary>
        /// How quickly the player deccelerates left and right when airborne.
        /// </summary>
        [SerializeField] private float aerialBrake = 25f;
        public float AerialBrake { get => aerialBrake; set => aerialBrake = value; }

        // Jumping
        //=========================================================================
        /// <summary>
        /// The gravity multiplier applied when the player falls after jumping and releasing the jump button.
        /// </summary>
        [SerializeField] private float fastFall = 7f;
        public float FastFall { get => fastFall; set => fastFall = value; }
        /// <summary>
        /// The standard gravity multiplier applied when the player falls or when the player jumps and does NOT release the jump button.
        /// </summary>
        [SerializeField] private float slowFall = 5f;
        public float SlowFall { get => slowFall; set => slowFall = value; }
        /// <summary>
        /// The max height in world units a player can jump.
        /// </summary>
        [SerializeField] private float jumpHeight = 4.4f;
        public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }
        /// <summary>
        /// The time it takes to reach the apex of a jump.
        /// </summary>
        [SerializeField] private float jumpApexTime = .4f;
        public float JumpApexTime { get => jumpApexTime; set => jumpApexTime = value; }
        /// <summary>
        /// The max height in world units a player can jump.
        /// </summary>
        [SerializeField] private float doubleJumpHeight = 4.4f;
        public float DoubleJumpHeight { get => doubleJumpHeight; set => doubleJumpHeight = value; }
        /// <summary>
        /// The time it takes to reach the apex of a jump.
        /// </summary>
        [SerializeField] private float doubleJumpApexTime = .4f;
        public float DoubleJumpApexTime { get => doubleJumpApexTime; set => doubleJumpApexTime = value; }
        /// <summary>
        /// The standard amount of jumps the player can perform.
        /// </summary>
        [SerializeField] private int totalJumps = 2;
        public int TotalJumps { get => totalJumps; set => totalJumps = value; }
        /// <summary>
        /// The base value for acceleration due to gravity.
        /// </summary>
        [SerializeField] private float baseGravity = -9.81f;
        public float BaseGravity { get => baseGravity; set => baseGravity = value; }



        // Wall Jumping
        //=========================================================================
        /// <summary>
        /// The speed at which the player slides down a wall.
        /// </summary>
        [SerializeField] private float wallSlideSpeed = -5f;
        public float WallSlideSpeed { get => wallSlideSpeed; set => wallSlideSpeed = value; }
        /// <summary>
        /// The X component of a player's wall jump. This value controls how quickly the player moves away from the wall.
        /// </summary>
        [SerializeField] private float wallJumpVelocity = 10f;
        public float WallJumpVelocity { get => wallJumpVelocity; set => wallJumpVelocity = value; }
        /// <summary>
        /// The maximum height a play can reach when performing a wall jump.
        /// </summary>
        [SerializeField] private float wallJumpHeight = 4f;
        public float WallJumpHeight { get => wallJumpHeight; set => wallJumpHeight = value; }
        /// <summary>
        /// The time it takes to reach the maxmimum height of a wall jump.
        /// </summary>
        [SerializeField] private float wallJumpApexTime = .3f;
        public float WallJumpApexTime { get => wallJumpApexTime; set => wallJumpApexTime = value; }
        /// <summary>
        /// The distance away from the wall a player will travel, before falling, while performing a wall jump.
        /// </summary>
        [SerializeField] private float wallJumpDistance = 4f;
        public float WallJumpDistance { get => wallJumpDistance; set => wallJumpDistance = value; }

        /// <summary>
        /// Note that Sliding, Grabbing, and Swinging are not implemented in the demo as they are still under development.
        /// </summary>
        // Sliding
        //=========================================================================
        [SerializeField] private float _slideBoost = 3f;
        public float SlideBoost { get => _slideBoost; set => _slideBoost = value; }
        [SerializeField] private float _slideBrakeForce = 1f;
        public float SlideBrakeForce { get => _slideBrakeForce; set => _slideBrakeForce = value; }
        // Grabbing and Swinging
        //=========================================================================
        [SerializeField] private float _grabBufferTime = 0.5f;
        public float GrabBufferTime { get => _grabBufferTime; set => _grabBufferTime = value; }
        [SerializeField] private float _grabRadius = 1f;
        public float GrabRadius { get => _grabRadius; set => _grabRadius = value; }
        [SerializeField] private Vector2 grabPoint = new Vector2(1f, 1.2f);
        public Vector2 GrabPoint { get => grabPoint; set => grabPoint = value; }
        [SerializeField] private Vector2 swingPoint = new Vector2(0f, 1.8f);
        public Vector2 SwingPoint { get => swingPoint; set => swingPoint = value; }
        [SerializeField] private float _swingJumpForce = 20f;
        public float SwingJumpForce { get => _swingJumpForce; set => _swingJumpForce = value; }

        // Buffers
        //=========================================================================
        /// <summary>
        /// The duration a wall jump will be buffered for.
        /// </summary>
        [SerializeField] private float wallJumpBufferTime = 0.5f;
        public float WallJumpBufferTime { get => wallJumpBufferTime; set => wallJumpBufferTime = value; }
        /// <summary>
        /// The time a player has to perform a jump after walking off of a surface.
        /// </summary>
        [SerializeField] private float coyoteTime = 0.25f;
        public float CoyoteTime { get => coyoteTime; set => coyoteTime = value; }
        /// <summary>
        /// The duration a wall jump will be buffered for.
        /// </summary>
        [SerializeField] private float jumpBufferTime = 0.2f;
        public float JumpBufferTime { get => jumpBufferTime; set => jumpBufferTime = value; }

        // Cool downs
        //=========================================================================
        /// <summary>
        /// NOT IMPLEMENTED IN DEMO
        /// </summary>
        [SerializeField] private float _boostCoolDown = 1f;
        public float BoostCoolDown { get => _boostCoolDown; set => _boostCoolDown = value; }

        // Teleport
        //=========================================================================

        /// <summary>
        /// How far the player can teleport from the current location. Range may vary depending on physical constraints.
        /// </summary>
        [SerializeField] private float _teleportRange = 5f;
        public float TeleportRange { get => _teleportRange; set => _teleportRange = value; }

        [SerializeField] private float _teleportCoolDown = 1f;
        public float TeleportCoolDown { get => _teleportCoolDown; set => _teleportCoolDown = value; }
        // I-Frames
        //=========================================================================
        /// <summary>
        /// How long the player is invincible for AFTER being hit.
        /// </summary>
        [SerializeField] private float _invincibilityTimer = 1f;
        public float InvincibilityDuration { get => _invincibilityTimer; set => _invincibilityTimer = value; }



    }

}