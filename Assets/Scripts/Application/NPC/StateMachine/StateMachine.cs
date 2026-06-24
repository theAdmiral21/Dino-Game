using UnityEngine;
using System;
using AI.Core.State.Abstractions;
using System.Collections.Generic;
using AI.Core.State.Enums;
using AI.Application.States;

namespace AI.Application.State
{
    /// <summary>
    /// This class drives the behavior of the enemy in its different states
    /// </summary>
    [System.Serializable]
    public class StateMachine<T> : IStateMachine<T>
    {
        public IState<T> CurrentState => _currentState;
        private IState<T> _currentState;
        private List<IState<T>> _states;
        public event Action<IState<T>> OnStateComplete;
        public event Action<IState<T>> OnStateChanged;
        private List<ITransition<T>> _transitions;

        public StateMachine(List<IState<T>> states, List<ITransition<T>> transitions)
        {
            _states = states;
            _transitions = transitions;

            // Default to the first state in the inspector list.
            _currentState = states[0];

        }

        public void ChangeState(IState<T> newState, T context)
        {
            // Debug.Log($"Changing from {_currentState} to {newState}");
            _currentState.Exit(context);
            _currentState = newState;
            _currentState.Enter(context);
            OnStateChanged?.Invoke(_currentState);
        }

        public void Enter(T context)
        {
            _currentState.Enter(context);
        }

        public void Exit(T context)
        {
            _currentState.Exit(context);
        }

        public void Update(T context)
        {
            // Debug.Log($"Current state: {CurrentState}");
            _currentState.Update(context);

            foreach (var transition in _transitions)
            {
                if ((transition.FromStates.Contains(CurrentState) || transition.FromStates == null) && transition.ToState != CurrentState)
                {
                    // Debug.Log($"Evaluating {transition.GetType().Name} - current state: {CurrentState.GetType().Name}");
                    bool res = transition.EvaluateTransition(context);
                    // Debug.Log($"transition {transition} result: {res}");
                    if (res)
                    {
                        OnStateComplete?.Invoke(CurrentState);
                        ChangeState(transition.ToState, context);
                        break;
                    }
                }
            }
        }
    }
}