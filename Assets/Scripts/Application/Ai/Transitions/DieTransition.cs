using System.Collections.Generic;
using UnityEngine;
using AI.Core.State;
using AI.Core.State.Abstractions;
using Game.Core.Health;

namespace AI.Application.Transitions
{
    public class DieTransition<T> : ITransition<T> where T : IDieContext
    {
        public IState<T> ToState => _toState;
        private IState<T> _toState;

        public HashSet<IState<T>> FromStates => _fromStates;

        private HashSet<IState<T>> _fromStates = new();

        public DieTransition(IState<T> toState)
        {
            _toState = toState;
            _fromStates = null;
        }

        public DieTransition(IState<T> toState, IState<T> fromState)
        {
            _toState = toState;
            _fromStates.Add(fromState);
        }
        public DieTransition(IState<T> toState, HashSet<IState<T>> fromState)
        {
            _toState = toState;
            _fromStates = fromState;
        }
        public bool EvaluateTransition(T context)
        {
            bool isAlive = context.CheckIsAlive();
            // Debug.Log($"isAlive: {isAlive}");
            return !isAlive;
        }
    }
}