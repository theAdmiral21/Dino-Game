using System;
using System.Collections.Generic;
using AI.Core.State;
using AI.Core.State.Abstractions;
using Movement.Core.Movement.DataStructures;
using UnityEngine;

namespace AI.Application.Transitions
{
    public class LostPlayerTransition<T> : ITransition<T> where T : IDetectPlayerContext, IMoveToContext
    {
        public IState<T> ToState => _toState;
        private IState<T> _toState;

        public HashSet<IState<T>> FromStates => _fromStates;

        private HashSet<IState<T>> _fromStates = new();

        public LostPlayerTransition(IState<T> toState, IState<T> fromState)
        {
            _toState = toState;
            _fromStates.Add(fromState);
        }
        public LostPlayerTransition(IState<T> toState, HashSet<IState<T>> fromStates)
        {
            _toState = toState;
            _fromStates = fromStates;
        }
        public bool EvaluateTransition(T context)
        {
            if (context.FoundPlayer)
            {
                return false;
            }
            else
            {
                Debug.Log($"Setting last known location");
                context.SetDestination(context.LastKnownLocation);
                return true;
            }
        }
    }
}