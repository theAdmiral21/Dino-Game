using UnityEngine;
using Movement.Core.Movement.Abstractions;
using NPC.Core.Effects;

namespace NPC.Unity.Effects
{
    public class RobotEffectTranslator : BaseEffectTranslator
    {
        public override void ConvertActionEffects(IActionResult effect)
        {
            // Debug.Log($"Robot got action result {effect}");
            switch (effect)
            {
                case AlertResult alert:
                    {
                        _effectResults.Add(new AlertEffect());
                        break;
                    }
                case PassiveResult passive:
                    {
                        _effectResults.Add(new PassiveEffect());
                        break;
                    }
                case DeathResult death:
                    {
                        _effectResults.Add(new DeathEffect());
                        break;
                    }
                case AttackResult attack:
                    {
                        _effectResults.Add(new AttackEffect());
                        break;
                    }
            }
        }
    }
}