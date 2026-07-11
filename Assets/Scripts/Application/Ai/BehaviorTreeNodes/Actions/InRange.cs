
using AI.Core.Behavior;
using UnityEngine;
using System.Collections.Generic;
using Core.Ai.Behavior.Visualization;
using System;
using Core.Ai.State.BehaviorContext;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Abstractions;
using Primitives.Stats.DataStructures;

namespace AI.Application.BehaviorTreeNodes
{
    public class InRange<T> : IBehaviorNode<T> where T : IPerceptionContext, IMoveToContext, IStatSheet
    {
        public string DisplayName => "InRange";
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
            // If we don't have a target return
            if (!context.Perception.TargetPosition.HasValue) return NodeResult.Failure;

            // Check distance to target
            float targetDist = TargetDistance(context);
            // This will need some thinking for future dinosaurs I think..
            float attackRange = context.StatCollection.Get<LungeStats>().LungeDistance.Value;
            if (targetDist <= attackRange)
            {
                return NodeResult.Success;
            }
            return NodeResult.Failure;
        }

        public void Reset(T context)
        {
            // Debug.Log($"Resetting InRange");
        }

        private float TargetDistance(T context)
        {
            float dist = Vector2.Distance(context.Perception.TargetPosition.Value, context.CurrentPosition);

            return dist;
        }
    }
}