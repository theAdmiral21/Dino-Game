
using AI.Core.Behavior;
using UnityEngine;
using System.Collections.Generic;
using Core.Ai.Behavior.Visualization;
using System;
using Core.Movement.Abstractions;
using Core.Movement.Inputs;
using Movement.Core.Abstractions;

namespace AI.Application.BehaviorTreeNodes
{
    public class Dead<T> : IBehaviorNode<T> where T : IDeadContext, IRaptorInputContext
    {
        public string DisplayName => "Dead";
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
            context.Die();
            context.RaptorInput.SetMove(Vector2.zero);
            return NodeResult.Success;
        }

        public void Reset(T context)
        {
            // Debug.Log($"Resetting Dead");
        }
    }
}