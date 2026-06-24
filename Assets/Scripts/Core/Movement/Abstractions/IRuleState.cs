using Core.Movement.Inputs;
using Movement.Core.State;
using Primitives.Physics;

namespace Movement.Core.Abstractions
{
    public interface IRuleState : IResetRuleState
    {
        public float Dt { get; }
        public void UpdateRules(IActorInput inputValues, PhysicsContext physicsContext, float dt);
    }
}