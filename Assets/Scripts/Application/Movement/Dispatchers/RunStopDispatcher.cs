using Movement.Application.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.Movement;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;


namespace Movement.Application.Dispatchers
{
    public class RunStopDispatcher : DispatchRequestBase<RunStopRequest, RunStopResult>
    {

        protected override RunStopResult Dispatch(in RunStopRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            return RunStopRules.TryStopRun(inputs, facts, ruleState);
        }
    }
}