using System.Collections.Generic;
using AI.Core.Behavior;

namespace AI.Application.BehaviorTreeNodes
{
    public class SequenceNode<T> : IBehaviorNode<T>
    {
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