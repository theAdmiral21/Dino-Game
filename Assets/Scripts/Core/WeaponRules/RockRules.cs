using Movement.Core.Abstractions;
using Movement.Core.Inputs;
using Primitives.Physics;

namespace Core.WeaponRules
{
    public class ThrowableRules : IRuleState,
                             IGravityState,
                             IFallState
    {
        public float Dt { get; private set; }

        public FallType FallType => _fallType;
        private FallType _fallType;

        public bool AffectedByGravity => true;
        public bool ApplyGravity => true;
        public ThrowableRules()
        {
            _fallType = FallType.None;
        }
        public void UpdateRules(IActorInput inputValues, PhysicsContext physicsContext, float dt)
        {
            Dt = dt;
        }

        public void ResetRuleState() { }
        public void SetApplyGravity(bool val) { }
        public void SetFallType(FallType type) => _fallType = type;


    }
}