using UnityEngine;
using AI.Core.Behavior;
using Movement.Core.Movement.DataStructures;
using AI.Core.State.BehaviorContext;
using AI.Core.Timers;
using AI.Application.Timers;
using Core.Ai.State.BehaviorContext;
using Core.Ai.BlackBoard;
using Application.Utility;
using Movement.Core.Abstractions;
using Core.Ai.BlackBoard.DataStructures;

namespace AI.Application.BehaviorTreeNodes
{
    public class Wander<T> : IBehaviorNode<T> where T : IMoveToContext, ITickTimerContext, IGameTimerContext, IStatusContext, IAlertContext, IInputContext
    {
        private float _maxWanderRadius = 10f;

        private float _wanderTime;
        public ITimerContext Timer { get; private set; }

        private Vector2 _lastPosition;
        private float _stuckCounter;
        private float _stuckTimer = 1f; // seconds before declaring stuck

        public Wander(float wanderTime = 3f)
        {
            _wanderTime = wanderTime;
            Timer = new TimerContext(_wanderTime);
        }
        public NodeResult Tick(T context)
        {
            context.SetStatus(Status.Searching);

            // If the timer is incomplete and inactive
            if (!Timer.TimerComplete && !Timer.IsActive)
            {
                Debug.Log($"Starting wander timer");
                // Pick a location
                PickDestination(context);
                // Restart the timer
                Timer.StartTimer();
            }

            // Only move while the timer is running
            if (Timer.IsActive)
            {
                Move(context);

                if (IsStuck(context)) return NodeResult.Failure;

                // Tick the timer 
                Timer.TickTimer(context.Dt);
                return NodeResult.Running;
            }

            // When the timer is complete
            return CalcPositionError(context) < 0.2f ? NodeResult.Success : NodeResult.Failure;
        }

        public void Reset(T context)
        {
            Timer.ResetTimer();
            context.AiInput.SetMove(Vector2.zero);
            _stuckCounter = 0;
            _lastPosition = Vector2.zero;
            Debug.Log($"Reset wander timer");
        }

        private void PickDestination(T context)
        {
            Vector2 dest = PathUtils.PickRandomXDest(-5f, _maxWanderRadius, context.CurrentPosition);
            Debug.Log($"Wandering to: {dest}");
            context.SetDestination(dest);
        }

        private void Move(T context)
        {
            float posError = CalcPositionError(context);
            if (posError > 0.2f)
            {
                // context.MoveTo();
                Vector2 dir = (context.Destination - context.CurrentPosition).normalized;
                // Debug.Log($"Run input vector: {dir}");
                context.AiInput.SetMove(dir);
            }
            else
            {
                context.AiInput.SetMove(Vector2.zero);
                // context.Stop();
            }
        }
        private bool IsStuck(T context)
        {
            // Stuck detection
            float moved = Vector2.SqrMagnitude(context.CurrentPosition - _lastPosition);
            // Debug.Log($"Stuck timer: {_stuckCounter}; Movement amount: {moved}");
            if (moved < 0.01f)
            {
                _stuckCounter += context.Dt;
                if (_stuckCounter >= _stuckTimer)
                {
                    _stuckCounter = 0f;
                    Debug.Log($"AI is stuck!");
                    return true; // give up, fall through to wander
                }
            }
            else
            {
                _stuckCounter = 0f;
            }

            _lastPosition = context.CurrentPosition;
            return false;
        }
        private float CalcPositionError(T context)
        {
            float posError = Vector2.SqrMagnitude(context.Destination - context.CurrentPosition);
            return posError;
        }
    }
}