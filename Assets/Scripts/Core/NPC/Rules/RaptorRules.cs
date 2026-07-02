using System;
using Core.Movement.Inputs;
using Enemy.Core.Rules;
using Movement.Core.Abstractions;
using Movement.Core.Stats;
using Primitives.Input;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace NPC.Core.Rules
{
    public class RaptorRules : EnemyRules,
                               ILungeState
    {
        public bool IsLunging => LungeCounter > 0;

        public int LungeAmount { get; private set; }
        private int _totalLunges;
        public float LungeTime { get; private set; }

        public float LungeCounter { get; private set; }

        public InputDirection LungeDirection { get; private set; }

        public RaptorRules(IStatCollection stats)
        {
            stats.TryGet<LungeStats>(out var lungeStats);
            _totalLunges = (int)lungeStats.TotalLunges.Value;
            LungeAmount = _totalLunges;
        }

        public new void UpdateRules(IActorInput inputValues, PhysicsContext physicsContext, float dt)
        {
            base.UpdateRules(inputValues, physicsContext, dt);
            TickTimers();

            if (physicsContext.IsGrounded || physicsContext.IsOnPlatform)
            {
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

        public void StartLungeTimer()
        {
            LungeCounter = LungeTime;
        }

        private void TickTimers()
        {
            if (LungeCounter > 0)
            {
                LungeCounter -= Dt;
            }
        }
    }
}
