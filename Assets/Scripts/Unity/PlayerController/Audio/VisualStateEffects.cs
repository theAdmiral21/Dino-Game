using System.Collections.Generic;
using Game.Core.Effects;
using Movement.Core.Abstractions;
using Movement.Core.Rules;
using PlayerController.Core.Effects.DataStructures;
using Primitives.Physics;
using UnityEngine;

namespace PlayerController.Unity.Audio
{
    public class VisualStateEffects : MonoBehaviour, IStateEffect
    {
        private bool _wasZooming;
        private bool _zoomiesFired;
        private List<IEffectResult> _results = new();
        public List<IEffectResult> EvaluateStateEffects(IRuleState actorRuleState, PhysicsContext physicsContext)
        {
            _results.Clear();

            EvaluateInvincible(actorRuleState);
            EvaluateZoomies(actorRuleState);

            return _results;
        }
        private void EvaluateInvincible(IRuleState ruleState)
        {
            if (!ruleState.TryGet<IInvincibleState>(out var invincible)) return;
            if (!ruleState.TryGet<IStunState>(out var stun)) return;

            if (invincible.IsInvincible || stun.IsStunned)
            {
                // trigger i frames
                _results.Add(new IFrameEffect(true));
            }
            else
            {
                // stop i frames
                _results.Add(new IFrameEffect(false));
            }
        }
        private void EvaluateZoomies(IRuleState ruleState)
        {
            // if (!ruleState.TryGet<IZoomiesState>(out var zoomies)) return;
            // Debug.Log("Evaluating zoomies");
            // if (zoomies.IsZooming) _results.Add(new ZoomiesEffect(true, zoomies.ZoomyAmount));

            // bool isZooming = zoomies.IsZooming;

            // if (isZooming && !_wasZooming)
            // {
            //     _results.Add(new ZoomiesEnterEffect(true));
            // }
            // else if (isZooming && _wasZooming && !_zoomiesFired)
            // {
            //     _results.Add(new ZoomiesTwinkleEffect(true));
            //     _zoomiesFired = true;
            // }
            // else if (!isZooming && _wasZooming)
            // {
            //     _results.Add(new ZoomiesExitEffect(true));
            //     _zoomiesFired = false;
            // }

            // _wasZooming = isZooming;
        }
    }
}