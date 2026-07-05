
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "IsAlive", menuName = "AI/Enemy/Behaviors/Raptor/Raptor IsAlive SO")]
    
    public class RaptorIsAliveSO : BehaviorNodeSO<RaptorContext>
    {
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new IsAlive<RaptorContext>();
        }
    }
}