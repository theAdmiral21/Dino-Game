
using AI.Core.Behavior;
using UnityEngine;
using System.Collections.Generic;
using Core.Ai.Behavior.Visualization;
using System;
using Core.Ai.State.BehaviorContext;
using Core.Ai.BlackBoard;
using Core.Ai.BlackBoard.DataStructures;
using Primitives.Health;
using PlayerController.Application.Physics.DataStructures;
using Primitives.Detection;
using AI.Core.State.BehaviorContext;
using Movement.Core.Movement.DataStructures;
using Core.Movement.Inputs;
using Movement.Core.Abstractions;
using AI.Core.State;

namespace AI.Application.BehaviorTreeNodes
{
    public class Stalk<T> : IBehaviorNode<T> where T : IAlertContext, IPerceptionContext, IStatusContext, IPackDataContext, IMoveToContext, IDetectorContext, IInputContext, ISearchAreaContext
    {
        public string DisplayName => "Stalk";
        public NodeResult LastResult { get; private set; }
        public float LastTickTime { get; private set; }
        public IReadOnlyList<IInspectableNode> Children => Array.Empty<IInspectableNode>();

        public NodeResult Tick(T context)
        {
            LastResult = TickInternal(context);
            LastTickTime = Time.time;
            return LastResult;
        }

        private NodeResult TickInternal(T context)
        {
            if (!context.Perception.TargetPosition.HasValue) return NodeResult.Failure;

            context.SetAlertLevel(AlertLevel.Interested);
            context.SetStatus(Status.Stalking);
            // notify pack mates you found something
            NotifyPack(context);

            // while stalking maintain distance, but follow the player
            Follow(context);
            return NodeResult.Running;
            // plan your next move
        }

        public void Reset(T context)
        {
            Debug.Log($"Resetting Stalk");
        }

        private void NotifyPack(T context)
        {
            float obsTime = Time.time;
            // Last known location
            context.PackData.LastKnownLocation = new Observation<Vector2>
            {
                Data = context.Perception.TargetPosition.Value,
                TimeOfObservation = obsTime,
            };

            // Target facing
            context.PackData.TargetFacing = new Observation<Vector2>
            {
                Data = context.Perception.TargetFacing.Value,
                TimeOfObservation = obsTime,
            };

            // Target health
            context.PackData.TargetHealth = new Observation<HealthState>
            {
                Data = context.Perception.TargetHealth,
                TimeOfObservation = obsTime,
            };

            // Target status
            context.PackData.TargetStatus = new Observation<PlayerStatus>
            {
                Data = context.Perception.TargetStatus,
                TimeOfObservation = obsTime,
            };
        }

        private void Follow(T context)
        {
            // keep the player at the edge of your vision
            Vector2 targetPos = context.PackData.LastKnownLocation.Value.Data;
            Vector2 targetDir = (targetPos - context.CurrentPosition).normalized;

            // adjust position to keep the player at around 95% of your maximum range
            float followDistance = context.DetectionStats.SightDistance * .7f;
            Vector2 followLocation = Vector2.right * (targetPos.x + (followDistance * -targetDir.x));

            Vector2 deltaX = followLocation - context.CurrentPosition;
            Vector2 deltaDir = deltaX.normalized;

            if (deltaX.sqrMagnitude > 0.5f)
            {
                if (deltaDir.x >= 0)
                {
                    context.AiInput.SetMove(deltaDir);
                }
                else
                {
                    context.AiInput.SetBackUp(deltaDir);
                }
            }
            else
            {
                context.Stop();
            }
        }

    }
}