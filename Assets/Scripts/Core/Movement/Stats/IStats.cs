using System;

namespace Movement.Core.Stats
{
    public interface IGameStat
    {
        Type StatType { get; }
    }
    public interface IRunStats : IGameStat
    {
        public float RunSpeed { get; }
        public float RunAccel { get; }
        public float BrakeAccel { get; }
    }

    public interface ISprintStats : IGameStat
    {
        public float SprintSpeed { get; }
        public float SprintAccel { get; }
    }

    public interface IAerialStats : IGameStat
    {
        public float AerialAccel { get; }
        public float AerialBrake { get; }
    }
    public interface IRotateStats : IGameStat
    {
        public float AngularVelocity { get; }
        public float AngularAccel { get; }
    }
    public interface IJumpStats : IGameStat
    {
        public int TotalJumps { get; }
        public float JumpHeight { get; }
        public float JumpApexTime { get; }
        public float JumpBufferTime { get; }
        public float CoyoteTime { get; }
    }

    public interface IGravityStats : IGameStat
    {
        public float BaseGravity { get; }
        public float SlowFall { get; }
        public float FastFall { get; }
    }

    public interface ILongJumpStats : IGameStat
    {
        public float LongJumpFarWindow { get; }
        public float LongJumpFarHeight { get; }
        public float LongJumpFarApexTime { get; }
        public float LongJumpFarSpeed { get; }
        public float LongJumpMedHeight { get; }
        public float LongJumpMedApexTime { get; }
        public float LongJumpMedSpeed { get; }
        // Should I add a buffer for this? <.<
    }

    public interface IDoubleJumpStats : IGameStat
    {
        public float DoubleJumpHeight { get; }
        public float DoubleJumpApexTime { get; }
    }

    public interface IWallStats : IGameStat
    {
        public float WallSlideSpeed { get; }
        public float WallJumpHeight { get; }
        public float WallJumpVelocity { get; }
        public float WallJumpApexTime { get; }
        public float WallJumpDistance { get; }
        public float WallJumpBufferTime { get; }
    }

    public interface IQuickStepStats : IGameStat
    {
        public float QuickStepDistance { get; }
        public float QuickStepDuration { get; }
        public float QuickStepCoolDown { get; }
    }

    public interface ITeleportStats : IGameStat
    {
        public float TeleportRange { get; }
        public float TeleportCoolDown { get; }
    }

    public interface IInvincibilityStats : IGameStat
    {
        public float InvincibilityDuration { get; }
    }

    public interface IZoomiesStats : IGameStat
    {
        // How much zoom the player can bank
        public float ZoomAmountLimit { get; }
        // How much zoom the player needs in order to get zoomies
        public float ZoomThreshold { get; }
        public float ZoomSpeed { get; }
        public float ZoomAccel { get; }
        public float ZoomBrakeAccel { get; }
    }
}