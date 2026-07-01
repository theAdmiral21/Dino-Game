
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "Stalk", menuName = "AI/Enemy/Behaviors/Raptor/Raptor Stalk SO")]
    
    public class RaptorStalkSO : BehaviorNodeSO<RaptorContext>
    {
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new Stalk<RaptorContext>();
        }
    }
}