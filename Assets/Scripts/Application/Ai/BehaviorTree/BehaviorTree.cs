using AI.Core.Behavior;
using UnityEngine;

namespace AI.Application.BehaviorTree
{
    public class BehaviorTree<T> : IBehaviorTree<T>
    {
        public IBehaviorNode<T> Root => _root;
        private IBehaviorNode<T> _root;

        public BehaviorTree(IBehaviorNode<T> root)
        {
            _root = root;
        }
        public void Tick(T context)
        {
            // Update your children
            _root.Tick(context);
        }
    }
}