using Movement.Application.Abstractions;
using Movement.Core.Enums;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Stats;
using Primitives.Physics;
using Core.Movement.Inputs;

namespace Movement.Features.Movement.Services
{
    public class WallSlideDispatcher : DispatchRequestBase<WallSlideRequest, WallSlideResult>
    {
        public PlayerStats Stats => _stats;
        private PlayerStats _stats;

        public WallSlideDispatcher(PlayerStats stats)
        {
            _stats = stats;
        }

        protected override WallSlideResult Dispatch(in WallSlideRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return new WallSlideResult(false, ActionPhase.Continuous);
        }
    }
}