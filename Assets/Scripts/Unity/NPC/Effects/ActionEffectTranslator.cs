using UnityEngine;
using Movement.Core.Movement.Abstractions;
using NPC.Core.Effects;
using Movement.Core.Movement.DataStructures;
using PlayerController.Core.Effects.DataStructures;

namespace NPC.Unity.Effects
{
    public class ActionEffectTranslator : BaseEffectTranslator
    {
        public override void ConvertActionEffects(IActionResult effect)
        {
            Debug.Log($"Raptor got action result {effect}");
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
                case LungeResult lunge:
                    {
                        _effectResults.Add(new LungeEffect());
                        break;
                    }
                case BiteResult bite:
                    {
                        _effectResults.Add(new BiteEffect());
                        break;
                    }
            }
        }
    }
}