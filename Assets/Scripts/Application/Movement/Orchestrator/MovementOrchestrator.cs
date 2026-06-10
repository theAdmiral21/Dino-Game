using System.Collections.Generic;
using System.Linq;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Abstractions;
using System;
using Movement.Core.Movement;
using Movement.Core.Rules;
using Physics.Core.DataStructures;

namespace Movement.Application
{
    /// <summary>
    /// Orchestrator that recieves Action Requests and evaluates them using Core rules.
    /// </summary>
    public class MovementOrchestrator
    {
        private ActionDispatcher _actionDispatcher;

        private List<IActionResult> _actionResults = new List<IActionResult>();


        public MovementOrchestrator(ActionDispatcher actionDispatcher)
        {
            _actionDispatcher = actionDispatcher;
        }
        public List<IActionResult> ProcessActions(ActorActionContext actionContext)
        {
            // Update core rules
            IRuleState ruleState = actionContext.RuleState as IRuleState;
            if (ruleState == null)
            {
                throw new ArgumentNullException("Unable to cast context rule state to IRuleState");
            }
            ruleState.UpdateRules(actionContext.InputValues, actionContext.Facts, actionContext.Dt);

            // Evaluate requested actions
            _actionResults = _actionDispatcher.DispatchActions(actionContext);

            // // Evaluate external/environmental effects
            var fallResult = FallRules.TryFall(actionContext.Facts, actionContext.InputValues, actionContext.RuleState);
            _actionResults.Add(fallResult);

            var landingStopResult = LandingRules.OnLand(actionContext.Facts, actionContext.RuleState, ref actionContext.InputValues);
            _actionResults.Add(landingStopResult);

            var wallStopResult = WallCollisionRules.OnContact(actionContext.Facts, actionContext.RuleState);
            _actionResults.Add(wallStopResult);

            var runStopResult = RunStopRules.TryStopRun(actionContext.InputValues, actionContext.Facts, actionContext.RuleState);
            _actionResults.Add(runStopResult);

            var quickStepUpdate = QuickStepUpdateRules.TryQuickStepUpdate(actionContext.Facts, actionContext.InputValues, actionContext.RuleState);
            _actionResults.Add(quickStepUpdate);

            var zoomieStop = StopZoomiesRules.TryStopZoomies(actionContext.Facts, actionContext.InputValues, actionContext.RuleState);
            _actionResults.Add(zoomieStop);

            // TODO figure out how to blend coming out of the quick step into other movement options.
            // var quickStepStopResult = QuickStepStopRules.TryQuickStepStop(actionContext.InputValues, actionContext.Facts, actionContext.RuleState);
            // _actionResults.Add(quickStepStopResult);

            // // After evaluation set values for the previous frame 
            IGroundedState groundState = actionContext.RuleState as IGroundedState;
            if (actionContext.RuleState.TryGet<IGroundedState>(out var result))
            {
                groundState.UpdateGroundedLastFrame(actionContext.Facts);
            }

            if (actionContext.RuleState.TryGet<IQuickStepState>(out var quickStepState))
            {
                quickStepState.UpdateQuickSteppingLastFrame();
            }

            var dashUpdate = DoggoDashUpdateRules.TryDoggoDashUpdate(actionContext.Facts, actionContext.InputValues, actionContext.RuleState);
            _actionResults.Add(dashUpdate);


            // sort the action results by phase priority. Impulse happens before continuous actions. ie Jump is calculated before Run.
            var orderedResults = _actionResults.OrderBy(r => r.Phase).ToList();

            return orderedResults;
        }
    }
}