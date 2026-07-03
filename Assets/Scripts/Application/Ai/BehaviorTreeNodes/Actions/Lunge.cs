
using AI.Core.Behavior;
using UnityEngine;
using System.Collections.Generic;
using Core.Ai.Behavior.Visualization;
using System;
using Movement.Core.Abstractions;
using Core.Ai.State.BehaviorContext;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Rules;
using Core.Movement.Inputs;
using Primitives.Stats.DataStructures;

namespace AI.Application.BehaviorTreeNodes
{
    public class Lunge<T> : IBehaviorNode<T> where T : IRaptorInputContext, IAlertContext, IPerceptionContext, IStatusContext, IPackDataContext, IMoveToContext, IStatSheet
    {
        public string DisplayName => "Lunge";
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
            // get in range of the target by running
            NodeResult res = ChargeTarget(context);

            return res;
            // make some scary noises

            // short hop with claws out

            // success vs failure depends on if you hit the player

        }

        public void Reset(T context)
        {
            // Debug.Log($"Resetting Lunge");
        }

        private NodeResult ChargeTarget(T context)
        {
            // get target data
            Vector2 targetPos = context.Perception.TargetPosition.Value;
            Vector2 targetDir = (targetPos - context.CurrentPosition).normalized;
            // pounce my girl
            // Debug.Log($"Lunging target dir: {targetDir}");
            context.RaptorInput.Lunge(targetDir);

            return NodeResult.Running;

            // making contact results in success, missing results in failure
        }
    }
}