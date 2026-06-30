using UnityEngine;
using AI.Core.Behavior;
using Core.Ai.State.BehaviorContext;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Abstractions;
using Core.Ai.BlackBoard;
using Core.Ai.BlackBoard.DataStructures;
using System.Collections.Generic;
using Core.Ai.Behavior.Visualization;
using System;

namespace Application.Ai.BehaviorTreeNodes.Actions
{
    public class Chase<T> : IBehaviorNode<T> where T : IInputContext, IPerceptionContext, IMoveToContext, IStatusContext, IPackDataContext
    {
        public string DisplayName => "Chase";
        public float LastTickTime { get; private set; }
        public NodeResult LastResult { get; private set; }
        public IReadOnlyList<IInspectableNode> Children => Array.Empty<IInspectableNode>();

        public void Reset(T context)
        {
            Debug.Log($"Resetting Chase behavior");
        }
        public NodeResult Tick(T context)
        {
            LastResult = TickInternal(context);
            return LastResult;
        }
        private NodeResult TickInternal(T context)
        {
            LastTickTime = Time.time;
            Debug.Assert(context.Perception != null, "Failed to set perception state");
            if (!context.Perception.TargetPosition.HasValue) return NodeResult.Failure;
            // We have spotted the player
            Debug.Log($"Chase ticking - TargetPosition: {context.Perception.TargetPosition}");
            if (context.Perception.TargetPosition.HasValue)
            {

                // You're really only attacking if you have a target
                context.SetStatus(Status.Attacking);
                // Update the pack data
                context.PackData.LastKnownLocation = new Observation<Vector2>
                {
                    Data = context.Perception.TargetPosition.Value,
                    TimeOfObservation = Time.time,
                };
                // pursue the player
                var chaseDir = (context.Perception.TargetPosition.Value - context.CurrentPosition).normalized;

                Move(context, chaseDir);
                return NodeResult.Running;
            }
            else
            {
                context.SetStatus(Status.Searching);
                return NodeResult.Failure;
            }
        }

        private void Move(T context, Vector2 dir)
        {
            context.AiInput.SetMove(dir);
        }
    }
}