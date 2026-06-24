using System.Collections.Generic;
using Game.Core.Effects;
using Movement.Core.Abstractions;
using Primitives.Physics;
using UnityEngine;

namespace NPC.Unity.Effects
{
    public class RobotVisualStateEffects : MonoBehaviour, IStateEffect
    {
        private List<IEffectResult> _results = new();
        public List<IEffectResult> EvaluateStateEffects(IRuleState ruleState, PhysicsContext physicsContext)
        {
            _results.Clear();

            return _results;
        }

    }
}