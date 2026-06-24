
using Core.Movement.Inputs;
using Movement.Core.Movement.DataStructures;
using Primitives.Physics;

namespace Movement.Core.Rules
{
    public static class LongJumpRules
    {
        public static LongJumpResult TryLongJump(LongJumpRequest request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {
            return new LongJumpResult();
        }

        private static LongJumpResult Approved()
        {
            return new LongJumpResult();
        }

        private static LongJumpResult Denied()
        {
            return new LongJumpResult();
        }

    }
}