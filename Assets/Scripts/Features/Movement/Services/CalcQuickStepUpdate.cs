using UnityEngine;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Features.Movement.Abstractions;
using Primitives.Physics;
using Movement.Core.Stats;
using Primitives.Stats.DataStructures;

namespace Movement.Features.Movement.Services
{
    public class CalcQuickStepUpdate : ICalcAction
    {

        public KinematicResult Calculate(IStatCollection stats, IActionResult actionResult, ref KinematicResult currentResult)
        {
            stats.TryGet<QuickStepStats>(out var _stats);
            if (actionResult is not QuickStepUpdateResult quickStepUpdate) return currentResult;

            if (!quickStepUpdate.Approved) return currentResult;

            // Ease that ish
            var quickState = quickStepUpdate.QuickStepState;

            /* NOTE Easing will change the area under the curve of your velocity function resulting in a smaller distance traveled. For example a linear easing, t, halves the area under the curve. In order to compensate for this sort of thing you have to add a comp factor. For linear, multiply the result by 2, for quadratic, multiply by 3, and for cubic, multiply by 4. I should draw some plots to get a feel for what this looks like.

            Okay I drew some plots of different functions from t = 0 -> 1. 
            
            When x is constant, the area = 1
            x^2, area == 1/3 x ^3
            x^3, area == 1/4 x ^4
            x^4, area == 1/5 x ^5

            ChatGPT says I can normalize the curve using the area, but that sounds like effort...

            tbh Seeing how integrals and derivatives all fit into this tickles my engineering brain. I should write an article on this and plot different things and see what I can learn.
            */

            float t = quickState.QuickStepCounter / quickState.QuickStepTime;
            float eased = t * t * t;
            // Debug.Log($"Eased val: {t} - Frame: {Time.frameCount}");

            float baseVelocity = _stats.QuickStepDistance.Value / _stats.QuickStepDuration.Value * quickState.QuickStepDir;
            // Debug.Log($"Base vel: {baseVelocity} - Frame: {Time.frameCount}");

            // currentResult.Velocity.x = baseVelocity * eased * 4f;
            // Debug.Log($"Result: {currentResult.Velocity.x} - Frame: {Time.frameCount}");

            float a = 0.5f;
            float b = 2f;

            float velocityFactor = a + b * eased;

            currentResult.Velocity.x = baseVelocity * velocityFactor;
            // Debug.Log($"Result: {currentResult.Velocity.x} - Frame: {Time.frameCount}");


            return currentResult;
        }

    }
}