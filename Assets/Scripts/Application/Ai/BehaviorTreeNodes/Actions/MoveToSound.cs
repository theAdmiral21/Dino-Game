using UnityEngine;
using AI.Core.Behavior;
using Core.Ai.State.BehaviorContext;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Abstractions;
using Core.Ai.BlackBoard;
using AI.Core.Timers;
using AI.Core.State.BehaviorContext;

namespace Application.Ai.BehaviorTreeNodes.Actions
{
    public class MoveToSound<T> : IBehaviorNode<T> where T : IInputContext, IPerceptionContext, IStatusContext, IMoveToContext, ITickTimerContext
    {
        private Vector2 _lastPosition;
        private float _stuckCounter;
        private float _stuckTimer = 1f; // seconds before declaring stuck
        public void Reset(T context)
        {
            // context.AiInput.SetMove(Vector2.zero);
            _stuckCounter = 0;
            _lastPosition = Vector2.zero;
        }

        public NodeResult Tick(T context)
        {
            context.SetStatus(Status.Tracking);
            Debug.Log($"MoveToSound status: {NodeResult.Failure}, Perception is null?");
            if (context.Perception == null) return NodeResult.Failure;
            // Check for audio data
            if (context.Perception.TimeOfAudio != 0)
            {
                // path towards it
                Move(context);
                if (IsStuck(context)) return NodeResult.Failure;

                Debug.Log($"MoveToSound status: {NodeResult.Running}");
                return NodeResult.Running;
            }
            else
            {
                Debug.Log($"MoveToSound status: {NodeResult.Failure}");
                return NodeResult.Failure;
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

        private void Move(T context)
        {
            context.AiInput.SetMove(context.Perception.AudioDirection.Value);
        }
    }
}