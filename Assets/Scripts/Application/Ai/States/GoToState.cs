using Movement.Core.Movement.DataStructures;
using AI.Core.State.Abstractions;
using AI.Core.State.Enums;
using AI.Core.State.BehaviorContext;
using UnityEngine;

namespace AI.Application.States
{
    /// <summary>
    /// A simple enemy state for moving back and forth over a set distance.
    /// </summary>
    public class GoToState<T> : IState<T> where T : IMoveToContext
    {


        public void Enter(T context)
        {
            Debug.Log($"Going to: {context.Destination}");
        }
        public void Update(T context)
        {
            context.MoveTo();
        }

        public void Exit(T context)
        {
            context.Stop();
        }

    }
}