using System.Collections.Generic;
using AI.Core.Behavior;
using UnityEngine;

namespace AI.Unity.BehaviorTree
{
    public abstract class BehaviorNodeSO : ScriptableObject { }

    public abstract class BehaviorNodeSO<T> : BehaviorNodeSO
    {
        public abstract IBehaviorNode<T> BuildRunTime();
    }


}