using Movement.Application.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.Movement;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;


namespace Movement.Application.Dispatchers
{
    public class RunDispatcher : DispatchRequestBase<RunRequest, RunResult>
    {

        protected override RunResult Dispatch(in RunRequest request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {
            // Debug.Log("Dispatching run");
            return RunRules.TryRun(request, facts, inputs, ruleState);

        }
    }
}