using System.Collections.Generic;
using System.Linq;
using AI.Core.Behavior;
using Core.Ai.Behavior.Visualization;
using UnityEngine;

namespace AI.Application.BehaviorTreeNodes
{
    public class SequenceNode<T> : IBehaviorNode<T>
    {
        public string DisplayName => "Sequence";
        public float LastTickTime { get; private set; }

        public NodeResult LastResult { get; private set; }

        public IReadOnlyList<IInspectableNode> Children => _children.Cast<IInspectableNode>().ToList();

        private List<IBehaviorNode<T>> _children;
        public SequenceNode(List<IBehaviorNode<T>> children)
        {
            _children = children;
        }

        public void Reset(T context)
        {
            for (int i = 0; i < _children.Count; i++)
            {
                var child = _children[i];
                child.Reset(context);
            }
        }

        public NodeResult Tick(T context)
        {
            LastTickTime = Time.time;

            // Check if anything passes
            for (int i = 0; i < _children.Count; i++)
            {
                var child = _children[i];

                NodeResult result = child.Tick(context);

                if (result == NodeResult.Failure)
                {
                    Reset(context);
                    return NodeResult.Failure;
                }

                if (result == NodeResult.Running) return NodeResult.Running;
            }
            return NodeResult.Success;
        }
    }
}