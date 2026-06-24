using System.Collections.Generic;
using AI.Core.State;
using AI.Core.State.Abstractions;
using Primitives.Detectors;

namespace AI.Application.Transitions
{
    public class DetectTransition<T> : ITransition<T> where T : IDetectPlayerContext
    {
        public IState<T> ToState => _toState;
        private IState<T> _toState;

        public HashSet<IState<T>> FromStates => _fromStates;

        private HashSet<IState<T>> _fromStates = new();
        public DetectTransition(IState<T> toState, IState<T> fromState)
        {
            _toState = toState;
            _fromStates.Add(fromState);
        }
        public DetectTransition(IState<T> toState, HashSet<IState<T>> fromStates)
        {
            _toState = toState;
            _fromStates = fromStates;
        }
        public bool EvaluateTransition(T context)
        {
            // Debug.Log($"Evaluating detect transition from: {string.Join(", ", FromStates.Select(s => s.GetType().Name) ?? new[] { "null (global)" })}");

            IDetectionData data = context.DetectPlayer();
            if (data != null)
            {
                if (data.ReadingQuality == DetectionReading.Current)
                {
                    context.EmitAlertEvent();
                    return true;
                }
            }
            context.EmitPassiveEvent();
            return false;
        }
    }
}