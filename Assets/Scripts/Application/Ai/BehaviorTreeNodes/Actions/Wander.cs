using UnityEngine;
using AI.Core.Behavior;
using Movement.Core.Movement.DataStructures;
using AI.Core.State.BehaviorContext;
using AI.Core.Timers;
using AI.Application.Timers;

namespace AI.Application.BehaviorTreeNodes
{
    public class Wander<T> : IBehaviorNode<T> where T : IMoveToContext, ITickTimerContext, IGameTimerContext
    {
        private float _maxWanderRadius = 10f;

        private float _wanderTime;
        public ITimerContext Timer { get; private set; }

        public Wander(float wanderTime = 3f)
        {
            _wanderTime = wanderTime;
            Timer = new TimerContext(_wanderTime);
        }
        public NodeResult Tick(T context)
        {
            // If the timer is incomplete and inactive
            if (!Timer.TimerComplete && !Timer.IsActive)
            {
                Debug.Log($"Starting wander timer");
                // Pick a location
                PickDestination(context);
                // Restart the timer
                Timer.StartTimer();

            }
            // The timer is incomplete and active
            else if (!Timer.TimerComplete && Timer.IsActive)
            {
                // Move in that direction
                Debug.Log($"Ticking wander timer");
                Move(context);
                // Tick the timer 
                Timer.TickTimer(context.Dt);

            }
            // The timer is complete and inactive
            else if (Timer.TimerComplete && !Timer.IsActive)
            {
                // reset the timer
                Timer.ResetTimer();
            }
            return EvaluateStatus(context);
        }

        private void PickDestination(T context)
        {
            // Get a random radius to pick
            float radius = Random.Range(5f, _maxWanderRadius);
            // Get a random angle
            float angle = Random.Range(0f, 2 * Mathf.PI);
            // Calculate the destination
            float x = radius * Mathf.Cos(angle);
            // float y = radius * Mathf.Sin(angle);
            context.SetDestination(context.CurrentPosition + new Vector2(x, 0));
        }

        private void Move(T context)
        {
            float posError = CalcPositionError(context);
            if (posError > 0.2f)
            {
                context.MoveTo();
            }
            else
            {
                context.Stop();
            }
        }

        private NodeResult EvaluateStatus(T context)
        {
            if (Timer.IsActive)
            {
                // Debug.Log($"Wander status: {NodeResult.Running}");
                return NodeResult.Running;
            }
            else
            {
                float posError = CalcPositionError(context);
                // Debug.Log($"Position error: {posError}");
                if (posError < 0.2f)
                {
                    // Debug.Log($"Wander status: {NodeResult.Success}");
                    return NodeResult.Success;
                }
                // Debug.Log($"Wander status: {NodeResult.Failure}");
                return NodeResult.Failure;
            }
        }

        private float CalcPositionError(T context)
        {
            float posError = Vector2.SqrMagnitude(context.Destination - context.CurrentPosition);
            return posError;
        }

        public void Reset(T context)
        {

        }
    }
}