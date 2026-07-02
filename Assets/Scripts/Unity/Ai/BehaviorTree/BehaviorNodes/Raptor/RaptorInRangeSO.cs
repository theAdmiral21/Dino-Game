
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "InRange", menuName = "AI/Enemy/Behaviors/Raptor/Raptor InRange SO")]
    
    public class RaptorInRangeSO : BehaviorNodeSO<RaptorContext>
    {
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new InRange<RaptorContext>();
        }
    }
}