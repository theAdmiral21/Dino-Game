using UnityEngine;
using AI.Core.Behavior;
using Core.Ai.State.BehaviorContext;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Abstractions;
using System.Net.Mime;

namespace Application.Ai.BehaviorTreeNodes.Actions
{
    public class MoveToSound<T> : IBehaviorNode<T> where T : IInputContext, IPerceptionContext
    {
        public void Reset(T context)
        {
            Debug.Log($"Resetting MoveToSound behavior");
        }

        public NodeResult Tick(T context)
        {
            Debug.Log($"MoveToSound status: {NodeResult.Failure}, Perception is null?");
            if (context.Perception == null) return NodeResult.Failure;
            // Check for audio data
            if (context.Perception.TimeOfAudio != 0)
            {
                // path towards it
                Move(context);
                Debug.Log($"MoveToSound status: {NodeResult.Running}");
                return NodeResult.Running;
            }
            else
            {
                Debug.Log($"MoveToSound status: {NodeResult.Failure}");
                return NodeResult.Failure;
            }
        }

        private void Move(T context)
        {
            context.AiInput.SetMove(context.Perception.AudioDirection.Value);
        }
    }
}