using System.Collections.Generic;
using AI.Core.Behavior;
using UnityEngine;

namespace AI.Unity.BehaviorTree
{
    public abstract class CompositeNodeSO<T> : BehaviorNodeSO<T>
    {
        [SerializeField] private List<BehaviorNodeSO> _children;

        protected List<IBehaviorNode<T>> BuildChildren()
        {
            var result = new List<IBehaviorNode<T>>();

            for (int i = 0; i < _children.Count; i++)
            {
                var child = _children[i];

                var typedChild = (BehaviorNodeSO<T>)child;
                result.Add(typedChild.BuildRunTime());
            }
            return result;
        }
    }

}