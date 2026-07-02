
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "Lunge", menuName = "AI/Enemy/Behaviors/Raptor/Raptor Lunge SO")]
    
    public class RaptorLungeSO : BehaviorNodeSO<RaptorContext>
    {
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new Lunge<RaptorContext>();
        }
    }
}