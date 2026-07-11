
using AI.Core.Behavior;
using UnityEngine;
using System.Collections.Generic;
using Core.Ai.Behavior.Visualization;
using System;
using Movement.Core.Abstractions;

namespace AI.Application.BehaviorTreeNodes
{
    public class BiteNode<T> : IBehaviorNode<T> where T : IRaptorInputContext
    {
        public string DisplayName => "Bite";
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
            context.RaptorInput.Bite();
            return NodeResult.Success;
        }

        public void Reset(T context)
        {
            // Debug.Log($"Resetting Bite");
        }
    }
}