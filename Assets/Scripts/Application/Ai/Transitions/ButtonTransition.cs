using UnityEngine;
using AI.Core.State.Abstractions;
using AI.Core.State.BehaviorContext;
using System.Collections.Generic;

namespace AI.Application.Transitions
{
    public class ButtonTransition<T> : ITransition<T>, ISwitchContext
    {
        public IState<T> ToState => _toState;
        private IState<T> _toState;

        public HashSet<IState<T>> FromStates => _fromStates;

        private HashSet<IState<T>> _fromStates = new();

        public bool IsActive => _isActive;
        private bool _isActive;
        public ButtonTransition(IState<T> toState, IState<T> fromState)
        {
            _toState = toState;
            _fromStates.Add(fromState);
        }
        public ButtonTransition(IState<T> toState, HashSet<IState<T>> fromStates)
        {
            _toState = toState;
            _fromStates = fromStates;
        }

        public bool EvaluateTransition(T context)
        {
            if (IsActive)
            {
                _isActive = false;
                return true;
            }
            return IsActive;
        }

        public void Switch()
        {
            _isActive = !_isActive;
        }
    }
}