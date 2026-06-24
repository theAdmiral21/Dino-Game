using AI.Core.State.Abstractions;
using AI.Core.State.Enums;

namespace AI.Application.States
{
    /// <summary>
    /// A simple enemy state for moving back and forth over a set distance.
    /// </summary>
    public class ColdState<T> : IState<T> where T : Movement.Core.Movement.DataStructures.IMoveToContext
    {
        public StateType State => StateType.Idle;

        public void Enter(T context)
        {
            context.Stop();
        }

        public void Update(T context)
        {
            context.Stop();
        }

        public void Exit(T context)
        {

        }


    }
}