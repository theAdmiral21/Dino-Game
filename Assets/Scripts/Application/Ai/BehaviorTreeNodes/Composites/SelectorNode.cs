using System.Collections.Generic;
using AI.Core.Behavior;

namespace AI.Application.BehaviorTreeNodes
{
    public class SelectorNode<T> : IBehaviorNode<T>
    {
        private List<IBehaviorNode<T>> _children;
        public SelectorNode(List<IBehaviorNode<T>> children)
        {
            _children = children;
        }

        public void Reset(T context)
        {
            throw new System.NotImplementedException();
        }

        public NodeResult Tick(T context)
        {
            // Check if anything passes
            for (int i = 0; i < _children.Count; i++)
            {
                var child = _children[i];

                NodeResult result = child.Tick(context);

                if (result == NodeResult.Success) return NodeResult.Success;

                if (result == NodeResult.Running) return NodeResult.Running;
                child.Reset(context);
            }
            return NodeResult.Failure;
        }
    }
}