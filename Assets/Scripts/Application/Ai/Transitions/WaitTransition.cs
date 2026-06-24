using UnityEngine;
using AI.Application.Timers;
using AI.Core.State.Abstractions;
using AI.Core.State.BehaviorContext;
using AI.Core.Timers;
using System.Collections.Generic;

namespace AI.Application.Transitions
{
    public class WaitTransition<T> : ITransition<T> where T : IGameTimerContext, ITickTimerContext
    {
        public IState<T> ToState => _toState;
        private IState<T> _toState;

        public HashSet<IState<T>> FromStates => _fromStates;

        private HashSet<IState<T>> _fromStates = new();
        public ITimerContext Timer { get; private set; }

        public WaitTransition(IState<T> toState, IState<T> fromState, float waitTime)
        {
            _toState = toState;
            _fromStates.Add(fromState);


            Timer = new TimerContext(waitTime);
            // Set the timer
            Timer.ResetTimer();
        }
        public WaitTransition(IState<T> toState, HashSet<IState<T>> fromStates, float waitTime)
        {
            _toState = toState;
            _fromStates = fromStates;


            Timer = new TimerContext(waitTime);
            // Set the timer
            Timer.ResetTimer();
        }

        public bool EvaluateTransition(T context)
        {
            // If the timer is incomplete and inactive
            if (!Timer.TimerComplete && !Timer.IsActive)
            {
                // Restart the timer
                Timer.StartTimer();
                Debug.Log($"Restarting timer");
                return false;
            }
            // The timer is incomplete and active
            else if (!Timer.TimerComplete && Timer.IsActive)
            {
                // Tick the timer 
                Timer.TickTimer(context.Dt);
            }
            // The timer is complete and inactive
            else if (Timer.TimerComplete && !Timer.IsActive)
            {
                // return true
                Timer.ResetTimer();
                return true;
            }
            return false;
        }
    }
}