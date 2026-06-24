using UnityEngine;
using AI.Core.State.Abstractions;
using Movement.Core.Enums;
using Movement.Core.Movement.DataStructures;
using System.Collections.Generic;

namespace AI.Application.Transitions
{
    public class ArriveTransition<T> : ITransition<T> where T : IMoveToContext
    {
        public IState<T> ToState => _toState;
        private IState<T> _toState;

        public HashSet<IState<T>> FromStates => _fromStates;

        private HashSet<IState<T>> _fromStates = new();

        private readonly Vector2 _stopSpeed = 0.5f * Vector2.one;
        public ArriveTransition(IState<T> toState, IState<T> fromState)
        {
            _toState = toState;
            _fromStates.Add(fromState);
        }
        public ArriveTransition(IState<T> toState, HashSet<IState<T>> fromStates)
        {
            _toState = toState;
            _fromStates = fromStates;
        }

        public bool EvaluateTransition(T context)
        {
            float posError;
            if (context.MoveType == MovementType.Run)
            {
                posError = CalcRunError(context);
                // Debug.Log($"Run error: {posError}");
                // Debug.Log($"PosError passes: {posError < 0.2f}");
                if (posError < 0.2f)// && Mathf.Abs(context.CurrentSpeed.x) <
                                    //_stopSpeed.x)
                {
                    // Debug.Log($"Arrived!");
                    return true;
                }
                // Debug.Log($"Did not arrive!");
                return false;
            }
            else
            {
                posError = CalcFlyError(context);
                // Debug.Log($"Fly error: {posError}");
                if (posError < 0.1f && Mathf.Abs(context.CurrentSpeed.x) <
                    _stopSpeed.x && Mathf.Abs(context.CurrentSpeed.y) < _stopSpeed.y)
                {
                    return true;
                }
                return false;
            }


        }

        private float CalcRunError(T context)
        {
            float posError = Mathf.Abs(context.Destination.x - context.CurrentPosition.x);
            return posError;
        }

        private float CalcFlyError(T context)
        {
            float posError = Vector2.SqrMagnitude(context.Destination - context.CurrentPosition);
            return posError;
        }
    }
}