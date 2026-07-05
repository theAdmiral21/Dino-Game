
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "Dead", menuName = "AI/Enemy/Behaviors/Raptor/Raptor Dead SO")]
    
    public class RaptorDeadSO : BehaviorNodeSO<RaptorContext>
    {
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new Dead<RaptorContext>();
        }
    }
}