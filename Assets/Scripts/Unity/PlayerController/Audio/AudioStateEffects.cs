using System.Collections.Generic;
using Movement.Core.Abstractions;
using Movement.Core.Rules;
using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Effects.DataStructures;
using Primitives.Physics;
using Primitives.Audio.Enums;
using UnityEngine;
using Game.Core.Effects;

namespace PlayerController.Unity.Audio
{
    public class AudioStateEffects : MonoBehaviour, IStateEffect
    {
        private bool _wasZooming;
        private bool _zoomiesFired;
        private List<IEffectResult> _results = new();
        public List<IEffectResult> EvaluateStateEffects(IRuleState actorRuleState, PhysicsContext physicsContext)
        {
            _results.Clear();

            // Evaluate effects
            EvaluateZoomies(actorRuleState);
            EvaluateWallSlide(physicsContext);

            return _results;
        }

        private void EvaluateZoomies(IRuleState ruleState)
        {
            // if (!ruleState.TryGet<IZoomiesState>(out var zoomies)) return;
            if (!ruleState.TryGet<IZoomiesState>(out var zoomies)) return;
            // Debug.Log("Evaluating zoomies");
            if (zoomies.IsZooming) _results.Add(new ZoomiesEffect(true, zoomies.ZoomyAmount));
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

        private void EvaluateWallSlide(PhysicsContext physicsContext)
        {
            _results.Add(new WallSlideEffect(physicsContext.IsWallSliding, physicsContext.Surface));
        }
    }
}