using Movement.Core.Abstractions;
using Movement.Core.Movement.Abstractions;

namespace Game.Core.Effects
{
    public interface IEffectTranslator
    {
        public void ConvertActionEffects(IActionResult effect);
        public void EvaluateStateEffects(IRuleState actorRuleState);
        // public void UpdateRuleState(IRuleState ruleState);
    }
}