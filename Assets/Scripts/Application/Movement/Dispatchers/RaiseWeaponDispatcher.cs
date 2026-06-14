
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Inputs;
using Movement.Core.Movement;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;

namespace Movement.Application.Dispatchers
{
    public sealed class RaiseWeaponDispatcher : DispatchRequestBase<RaiseWeaponRequest, RaiseWeaponResult>
    {
        protected override RaiseWeaponResult Dispatch(in RaiseWeaponRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return RaiseWeaponRules.TryRaiseWeapon(request, facts, inputs, ruleState);
        }
    }
}