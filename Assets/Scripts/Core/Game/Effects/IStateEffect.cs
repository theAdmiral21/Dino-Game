using System.Collections.Generic;
using Movement.Core.Abstractions;
using Primitives.Physics;

namespace Game.Core.Effects
{
    public interface IStateEffect
    {
        public List<IEffectResult> EvaluateStateEffects(IRuleState actorRuleState, PhysicsContext physicsContext);
    }
}