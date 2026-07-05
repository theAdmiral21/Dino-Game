
using AI.Core.Behavior;
using UnityEngine;
using System.Collections.Generic;
using Core.Ai.Behavior.Visualization;
using System;
using Core.Movement.Abstractions;

namespace AI.Application.BehaviorTreeNodes
{
    public class IsAlive<T> : IBehaviorNode<T> where T : IHealthContext
    {
        public string DisplayName => "IsAlive";
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
            //if alive return failure
            if (context.HealthComponent.IsAlive)
            {
                // Don't enter the death sequence, you're alive
                return NodeResult.Failure;
            }
            return NodeResult.Success;

        }

        public void Reset(T context)
        {
            Debug.Log($"Resetting IsAlive");
        }
    }
}