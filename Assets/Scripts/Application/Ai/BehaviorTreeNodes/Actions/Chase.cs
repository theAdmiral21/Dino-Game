using UnityEngine;
using AI.Core.Behavior;
using Core.Ai.State.BehaviorContext;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Abstractions;
using System.Net.Mime;

namespace Application.Ai.BehaviorTreeNodes.Actions
{
    public class Chase<T> : IBehaviorNode<T> where T : IInputContext, IPerceptionContext, IMoveToContext
    {
        public void Reset(T context)
        {
            Debug.Log($"Resetting Chase behavior");
        }

        public NodeResult Tick(T context)
        {
            Debug.Assert(context.Perception != null, "Failed to set perception state");

            // We have spotted the player
            Debug.Log($"Knows target position: {context.Perception.TargetPosition.HasValue}");
            if (context.Perception.TargetPosition.HasValue)
            {
                // pursue the player
                var chaseDir = (context.Perception.TargetPosition.Value - context.CurrentPosition).normalized;

                Move(context, chaseDir);
                return NodeResult.Running;
            }
            else
            {
                return NodeResult.Failure;
            }
        }

        private void Move(T context, Vector2 dir)
        {
            context.AiInput.SetMove(dir);
        }
    }
}