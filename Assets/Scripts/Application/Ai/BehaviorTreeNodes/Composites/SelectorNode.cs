using System.Collections.Generic;
using UnityEngine;
using AI.Core.Behavior;
using Core.Ai.Behavior.Visualization;
using System.Linq;

namespace AI.Application.BehaviorTreeNodes
{
    public class SelectorNode<T> : IBehaviorNode<T>
    {

        public string DisplayName => "Selector";
        public float LastTickTime { get; private set; }

        public NodeResult LastResult { get; private set; }

        public IReadOnlyList<IInspectableNode> Children => _children.Cast<IInspectableNode>().ToList();

        public string CurrentNode => _currentNode;
        private string _currentNode;
        private List<IBehaviorNode<T>> _children;
        public SelectorNode(List<IBehaviorNode<T>> children)
        {
            _children = children;
        }

        public void Reset(T context)
        {
            Debug.LogError($"What should this reset?");
        }

        public NodeResult Tick(T context)
        {
            LastTickTime = Time.time;

            // Check if anything passes
            for (int i = 0; i < _children.Count; i++)
            {
                var child = _children[i];

                _currentNode = $"{child}";

                NodeResult result = child.Tick(context);

                if (result == NodeResult.Success) return NodeResult.Success;

                if (result == NodeResult.Running) return NodeResult.Running;
                child.Reset(context);
            }
            return NodeResult.Failure;
        }
    }
}