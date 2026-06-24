using System.Collections.Generic;
using Game.Core.Effects;
using Movement.Core.Abstractions;
using Primitives.Physics;
using UnityEngine;

namespace NPC.Unity.Effects
{
    public class RobotAudioStateEffects : MonoBehaviour, IStateEffect
    {
        public List<IEffectResult> EvaluateStateEffects(IRuleState ruleState, PhysicsContext physicsContext)
        {
            throw new System.NotImplementedException();
        }
    }
}