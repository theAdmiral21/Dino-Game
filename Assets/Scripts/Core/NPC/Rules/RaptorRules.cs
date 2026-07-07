using System;
using Core.Movement.Inputs;
using Enemy.Core.Rules;
using Movement.Core.Abstractions;
using Movement.Core.Stats;
using Primitives.Input;
using Primitives.Physics;
using Primitives.Physics.Enums;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace NPC.Core.Rules
{
    public class RaptorRules : EnemyRules,
                               ILungeState,
                               IClimbState
    {
        // public bool IsLunging => LungeCounter > 0;

        public int LungeAmount { get; private set; }
        private int _totalLunges;

        public InputDirection LungeDirection { get; private set; }

        public float LungeCoolDownTime { get; private set; }

        public float LungeCoolDownCounter { get; private set; }

        public bool IsClimbing => false;

        public ClimbObject ClimbingSurface => ClimbObject.None;

        public bool WasClimbingLastFrame => false;

        public RaptorRules(IStatCollection stats)
        {
            stats.TryGet<LungeStats>(out var lungeStats);
            _totalLunges = (int)lungeStats.TotalLunges.Value;
            LungeAmount = _totalLunges;

            LungeCoolDownTime = lungeStats.LungeCoolDown.Value;

        }

        public override void UpdateRules(IActorInput inputValues, PhysicsContext physicsContext, float dt)
        {
            base.UpdateRules(inputValues, physicsContext, dt);

            TickTimers();
            Debug.Log($"Called tick timers");
            ResetLunges(physicsContext);

        }
        private void ResetLunges(PhysicsContext physicsContext)
        {
            if ((physicsContext.IsGrounded || physicsContext.IsOnPlatform) && LungeCoolDownCounter <= 0)
            {
                Debug.Log($"Reset lunges");
                LungeAmount = _totalLunges;
            }
        }
        public void DecrementLunge()
        {
            LungeAmount -= 1;
            if (LungeAmount < 0) LungeAmount = 0;
        }

        public void ResetLunge(PhysicsContext physicsContext)
        {
            LungeAmount = _totalLunges;
        }

        public void SetLungeDirection(InputDirection direction)
        {
            LungeDirection = direction;
        }

        public void StartLungeCoolDownTimer()
        {
            Debug.Log($"Starting lunge cool down");
            LungeCoolDownCounter = LungeCoolDownTime;
        }

        private void TickTimers()
        {
            if (LungeCoolDownCounter > 0)
            {
                Debug.Log($"CoolDown counter: {LungeCoolDownCounter}");
                LungeCoolDownCounter -= Dt;
            }
        }

        public void SetClimbing(bool val)
        {

        }

        public void SetClimbingSurface(ClimbType climbingSurface)
        {
        }

        public void UpdateClimbingState(PhysicsContext physicsContext)
        {
        }
    }
}
