using UnityEngine;
using AI.Core.State;
using AI.Core.State.Abstractions;
using AI.Core.State.Enums;
using AI.Core.Timers;
using AI.Core.State.BehaviorContext;
using AI.Application.Timers;

namespace AI.Application.States
{
    public class SearchState<T> : IState<T> where T : ISearchAreaContext, ITickTimerContext
    {
        public StateType State => StateType.Search;

        private ITimerContext _timer;
        private float _dirInterval;
        private bool _lookLeft;
        public SearchState(float interval = 1f)
        {
            // you can set the time but you don't have to
            _dirInterval = interval;
            _timer = new TimerContext(_dirInterval);
        }

        public void Enter(T context)
        {
            Debug.Log($"Starting search!");
            _timer.ResetTimer();
        }

        public void Update(T context)
        {
            Debug.Log($"Updating search!");
            // Every time the timer starts over flip the direction
            if (!_timer.IsActive && !_timer.TimerComplete)
            {
                _lookLeft = !_lookLeft;
                _timer.StartTimer();
                Debug.Log($"Starting timer!");
            }

            if (_timer.IsActive && !_timer.TimerComplete)
            {
                _timer.TickTimer(context.Dt);
            }

            if (_lookLeft)
            {
                context.FaceLeft();
            }
            else
            {
                context.FaceRight();
            }
            context.SearchArea();

            if (!_timer.IsActive && _timer.TimerComplete)
            {
                _timer.ResetTimer();
                Debug.Log($"Resetting search timer!");
            }
        }
        public void Exit(T context)
        {
            Debug.Log($"Stopping search!");
            _timer.StopTimer();
        }
    }
}