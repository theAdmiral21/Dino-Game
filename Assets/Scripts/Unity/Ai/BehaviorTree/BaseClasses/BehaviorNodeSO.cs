using AI.Core.Behavior;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    public abstract class BehaviorNodeSO : ScriptableObject { }

    public abstract class BehaviorNodeSO<T> : BehaviorNodeSO
    {
        public abstract IBehaviorNode<T> BuildRunTime();
    }


}