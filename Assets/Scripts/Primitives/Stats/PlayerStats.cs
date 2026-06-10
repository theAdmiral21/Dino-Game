namespace Primitives.Stats
{
    public struct PlayerStats
    {
        // public Stat GroundSpeedLimit;
        public Stat RunSpeed;
        public Stat RunAccel;
        public Stat RunAccelTime;
        public Stat BrakeAccel;
        public Stat SlowFall;
        public Stat FastFall;
        public Stat AerialAccel;
        // public Stat AerialAccelTime;
        public Stat AerialBrake;
        public Stat JumpHeight;
        public Stat JumpApexTime;
        public Stat DoubleJumpHeight;
        public Stat DoubleJumpApexTime;
        public Stat WallSlideSpeed;
        public Stat WallJumpHeight;
        public Stat WallJumpVelocity;
        public Stat WallJumpApexTime;
        public Stat SprintSpeed;
        public Stat SprintAccel;
        // public Stat QuickStepSpeed;
        public Stat QuickStepDistance;
        public Stat QuickStepDuration;
        public Stat QuickStepCoolDown;
        public Stat LongJumpWindow;

        // public Stat SlideBoost;
        // public Stat SlideBrakeForce;
        // public Stat GrabBufferTime;
        // public Stat GrabRadius;
        public Stat JumpBufferTime;
        public Stat WallJumpBufferTime;
        public Stat WallJumpDistance;
        public Stat CoyoteTime;
        public Stat BaseGravity;
        // public Stat JumpGravity;
        public Stat TotalJumps;

        // The timer the player is invincible after being hit.
        public Stat InvincibilityTimer;
        public Stat TeleportCooldown;
        public Stat TeleportRange;

        // Using this interface here is DEFINITELY a smell...
        // public PlayerStats(IMovementStats stats)
        // {
        // SprintSpeed = new Stat(stats.SprintSpeed);
        // SprintAccel = new Stat(stats.SprintAccel);

        // RunSpeed = new Stat(stats.RunSpeed);
        // RunAccel = new Stat(stats.RunAccel);
        // RunAccelTime = new Stat(stats.RunAccel);
        // BrakeAccel = new Stat(stats.BrakeAccel);

        // SlowFall = new Stat(stats.SlowFall);
        // FastFall = new Stat(stats.FastFall);
        // AerialAccel = new Stat(stats.AerialAccel);
        // // AerialAccelTime = new Stat(stats.AerialAccelTime);
        // AerialBrake = new Stat(stats.AerialBrake);

        // JumpHeight = new Stat(stats.JumpHeight);
        // JumpApexTime = new Stat(stats.JumpApexTime);
        // DoubleJumpHeight = new Stat(stats.DoubleJumpHeight);
        // DoubleJumpApexTime = new Stat(stats.DoubleJumpApexTime);
        // TotalJumps = new Stat(stats.TotalJumps);

        // WallSlideSpeed = new Stat(stats.WallSlideSpeed);

        // WallJumpHeight = new Stat(stats.WallJumpHeight);
        // WallJumpVelocity = new Stat(stats.WallJumpVelocity);
        // WallJumpApexTime = new Stat(stats.WallJumpApexTime);
        // WallJumpDistance = new Stat(stats.WallJumpDistance);

        // CoyoteTime = new Stat(stats.CoyoteTime);
        // JumpBufferTime = new Stat(stats.JumpBufferTime);
        // WallJumpBufferTime = new Stat(stats.WallJumpBufferTime);

        // BaseGravity = new Stat(stats.BaseGravity);

        // QuickStepDistance = new Stat(stats.QuickStepDistance);
        // QuickStepDuration = new Stat(stats.QuickStepDuration);
        // QuickStepCoolDown = new Stat(stats.QuickStepCoolDown);
        // LongJumpWindow = new Stat(stats.LongJumpWindow);

        // InvincibilityTimer = new Stat(stats.InvincibilityDuration);

        // TeleportRange = new Stat(stats.TeleportRange);
        // TeleportCooldown = new Stat(stats.TeleportCoolDown);
        // }
    }


}