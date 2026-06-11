using Movement.Core.Abstractions;
using Movement.Core.Movement.Abstractions;

namespace PlayerController.Application.Effects.Abstractions
{
    public interface IPlayerEffectOrchestrator
    {
        public void EvaluateActionEffects(IActionResult effect);
        public void EvaluateStateEffects(IRuleState actorRuleState);
        public void SetRuleState(IRuleState playerRuleState);
    }
}