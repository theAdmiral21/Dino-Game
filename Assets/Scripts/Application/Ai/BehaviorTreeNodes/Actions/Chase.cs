using UnityEngine;
using AI.Core.Behavior;
using Core.Ai.State.BehaviorContext;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Abstractions;
using Core.Ai.BlackBoard;

namespace Application.Ai.BehaviorTreeNodes.Actions
{
    public class Chase<T> : IBehaviorNode<T> where T : IInputContext, IPerceptionContext, IMoveToContext, IStatusContext
    {
        public void Reset(T context)
        {
            Debug.Log($"Resetting Chase behavior");
        }

        public NodeResult Tick(T context)
        {
            Debug.Assert(context.Perception != null, "Failed to set perception state");
            if (context.Perception == null) return NodeResult.Failure;
            // We have spotted the player
            // Debug.Log($"Knows target position: {context.Perception.TargetPosition.HasValue}");
            if (context.Perception.TargetPosition.HasValue)
            {
                Debug.Log($"Target found");
                // You're really only attacking if you have a target
                context.SetStatus(Status.Attacking);
                // pursue the player
                var chaseDir = (context.Perception.TargetPosition.Value - context.CurrentPosition).normalized;

                Move(context, chaseDir);
                return NodeResult.Running;
            }
            else
            {
                Debug.Log($"Target lost");
                return NodeResult.Failure;
            }
        }

        private void Move(T context, Vector2 dir)
        {
            context.AiInput.SetMove(dir);
        }
    }
}