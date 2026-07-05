using UnityEngine;
using AI.Core.Behavior;
using Core.Ai.State.BehaviorContext;
using Movement.Core.Abstractions;
using Core.Ai.BlackBoard;
using Movement.Core.Movement.DataStructures;
using Core.Ai.BlackBoard.DataStructures;
using AI.Core.State.BehaviorContext;
using System.Collections.Generic;
using Core.Ai.Behavior.Visualization;
using System;

namespace Application.Ai.BehaviorTreeNodes.Actions
{
    public class MoveToPackTarget<T> : IBehaviorNode<T> where T : IAInputContext, IPackDataContext, IStatusContext, IMoveToContext, ITickTimerContext
    {
        public string DisplayName => "MoveToPackTarget";
        public float LastTickTime { get; private set; }
        public NodeResult LastResult { get; private set; }
        public IReadOnlyList<IInspectableNode> Children => Array.Empty<IInspectableNode>();


        private float _supportRadius = 7f;
        private float _staleAge = 3; // seconds
        private Vector2 _lastPosition;
        private float _stuckCounter;
        private float _stuckTimer = 1f; // seconds before declaring stuck
        public void Reset(T context)
        {
            Debug.Log($"Resetting MoveToTarget behavior");
            // context.AiInput.SetMove(Vector2.zero);
            _stuckCounter = 0;
            _lastPosition = Vector2.zero;
        }

        public NodeResult Tick(T context)
        {
            LastResult = TickInternal(context);
            return LastResult;
        }
        private NodeResult TickInternal(T context)
        {
            LastTickTime = Time.time;

            if (!context.PackData.LastKnownLocation.HasValue) return NodeResult.Failure;
            if (context.PackData.LastKnownLocation.Value.IsStale(_staleAge)) return NodeResult.Failure;

            Observation<Vector2> lastKnownObs = context.PackData.LastKnownLocation.Value;

            // distance from the target
            float dist = Vector2.Distance(context.CurrentPosition, lastKnownObs.Data);


            // If the data is fresh and you're too far away to help, move to support
            if (!lastKnownObs.IsStale(_staleAge) && (dist > _supportRadius))
            {
                // A pack mate has spotted the target
                context.SetStatus(Status.Reinforcing);
                // Until I develop real pathing just run towards the location
                Move(context);

                if (IsStuck(context)) return NodeResult.Failure;

                return NodeResult.Running;
            }
            // You've arrived and the data is still good
            else if (!lastKnownObs.IsStale(_staleAge) && (dist < _supportRadius))
            {
                return NodeResult.Success;
            }
            // You never arrived or the data went stale
            else
            {
                return NodeResult.Failure;
            }
        }

        private void Move(T context)
        {
            Vector2 lastKnown = context.PackData.LastKnownLocation.Value.Data;
            Vector2 bearing = (lastKnown - context.CurrentPosition).normalized;
            bearing.y = context.CurrentPosition.y;
            context.AiInput.SetMove(bearing);
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
    }
}