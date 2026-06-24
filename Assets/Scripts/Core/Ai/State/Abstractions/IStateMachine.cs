using System;

namespace AI.Core.State.Abstractions
{
    public interface IStateMachine<T>
    {
        public IState<T> CurrentState { get; }
        public void ChangeState(IState<T> newState, T context);
        public void Enter(T context);
        public void Update(T context);
        public void Exit(T context);


        /// <summary>
        /// Event that is raised when a state is exited. Returns the completed state.
        /// </summary>
        public event Action<IState<T>> OnStateComplete;
        /// <summary>
        /// Event that is raised when a state is entered. Returns the entered state.
        /// </summary>
        public event Action<IState<T>> OnStateChanged;
    }
}