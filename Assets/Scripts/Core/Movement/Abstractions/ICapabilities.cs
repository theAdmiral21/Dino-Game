using Core.Movement.Inputs;
using Primitives.Input;
using Primitives.Physics;
using Primitives.Physics.Enums;

namespace Movement.Core.Abstractions
{
    public interface IJumpState
    {

        public float RemainingJumps { get; }
        public bool CanCoyoteJump { get; }
        public bool JumpBuffered { get; }
        public void DecrementJumps();
        public void ResetJumps(PhysicsContext physicsContext);
        public void StartJumpBufferTimer();
        public void ResetBufferTimer();
        public void StartCoyoteTimer(PhysicsContext physicsContext);
    }


    public interface IXInputState
    {
        public bool XInputLocked { get; }
        public void StartBlockXTimer();
    }

    public interface IGroundedState
    {
        public bool GroundedLastFrame { get; }
        public float UngroundedCounter { get; }
        public void UpdateGroundedLastFrame(PhysicsContext physicsContext);
        public void StartUngroundedTimer(PhysicsContext physicsContext);
    }

    public interface IDirectionState
    {
        public bool IsLocked { get; }
        public float Dir { get; }
        public void SetDirection(IActorInput input, PhysicsContext physicsContext);
        public void ForceDirection(bool left);
        public void LockDirection(bool locked);
    }

    public interface IFallState
    {
        public FallType FallType { get; }
        public void SetFallType(FallType fallType);
    }

    public interface IGravityState
    {
        public bool AffectedByGravity { get; }
        public bool ApplyGravity { get; }
        public void SetApplyGravity(bool val);
    }
    public interface IStunState
    {
        public bool IsStunned { get; }
        public void StartStunnedTimer(float duration);
    }


    public interface IInvincibleState
    {
        public bool IsInvincible { get; }
        public void StartIFrameTimer();

        public void UpdateDodgeInvincibility();
    }

    // public interface ITeleportState
    // {
    //     public bool CanTeleport { get; }
    //     public void StartTeleportCoolDown();
    // }

    public interface IDisabledState
    {
        public bool IsDisabled { get; }
        public void SetDisabled(bool val);
    }

    public interface ILandingState
    {
        public bool LandingStopRequested { get; set; }
    }

    public interface IQuickStepState
    {
        public float QuickStepDir { get; }
        public float QuickStepTime { get; }
        public float QuickStepCounter { get; }
        public float QuickStepCoolDown { get; }
        public bool QuickStepActive { get; }
        public bool QuickStepReady { get; }
        public bool QuickSteppingLastFrame { get; }
        public void StartQuickStepTimer();
        public void StartQuickStepCoolDown();

        public void UpdateQuickSteppingLastFrame();
        public void SetQuickStepDirection(float dir);
    }

    // public interface ILongJumpState
    // {
    //     public bool LongJumpIsActive { get; }
    //     public float LongJumpCounter { get; }
    //     public bool CanLongJump { get; }
    //     public float LongJumpFarWindow { get; }
    //     public void StartLongJumpTimer();
    //     public void UpdateLongJumpState();
    // }

    // public interface IZoomiesState
    // {
    //     // How much zoom the player can bank
    //     public float ZoomLimit { get; }
    //     // How much zoom the player needs in order to get zoomies
    //     public float MinimumRequiredZoom { get; }
    //     public bool IsZooming { get; }
    //     public float ZoomyAmount { get; }
    //     public void AddZoomies(float zoomAmount);
    //     public void StartZoomiesTimer();
    // }

    public interface IDodgeState
    {
        public bool IsDodging { get; }
        public int DodgeAmount { get; }
        public float DodgeTime { get; }
        public float DodgeCounter { get; }
        public InputDirection DodgeDirection { get; }
        public void StartDodgeTimer();
        public void DecrementDodge();
        public void ResetDodge(PhysicsContext physicsContext);
        public void SetDodgeDirection(InputDirection direction);
    }

    // public interface ISwitchMovement
    // {
    //     public bool DashMode { get; }
    //     public void SwitchMovement();
    // }

    public interface ICrouchState
    {
        public bool IsCrouching { get; }
        public void SetCrouchState(bool val);
        public void UpdateCrouchState(IActorInput inputValues);
    }

    public interface IAimingState
    {
        public bool IsAiming { get; }
        public void SetAiming(bool val);
    }

    public interface IFrictionState
    {
        public float Friction { get; }
    }

    public interface ILungeState
    {
        // public bool IsLunging { get; }
        public int LungeAmount { get; }
        public float LungeCoolDownTime { get; }
        public float LungeCoolDownCounter { get; }
        public InputDirection LungeDirection { get; }
        public void StartLungeCoolDownTimer();
        public void DecrementLunge();
        public void ResetLunge(PhysicsContext physicsContext);
        // public void SetLungeDirection(InputDirection direction);
    }

    public interface IClimbState
    {
        public bool IsClimbing { get; }
        public bool WasClimbingLastFrame { get; }
        public ClimbObject ClimbingSurface { get; }
        public void SetClimbing(bool val);
        public void SetClimbingSurface(ClimbType climbingSurface);
        public void UpdateClimbingState(PhysicsContext physicsContext);
    }
}