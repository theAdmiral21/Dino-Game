using UnityEngine;
using AI.Core.State;
using AI.Core.State.Abstractions;
using AI.Core.State.Enums;
using Movement.Core.Movement.DataStructures;
using UnityEngine.UIElements;

namespace AI.Application.States
{
    /// <summary>
    /// A simple enemy state for moving back and forth over a set distance.
    /// </summary>
    public class DeadState<T> : IState<T> where T : IDieContext, IMoveToContext
    {
        public StateType State => StateType.Dead;

        public void Enter(T context)
        {
            Debug.Log($"Something died!");
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