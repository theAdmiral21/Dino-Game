
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "Pounce", menuName = "AI/Enemy/Behaviors/Raptor/Raptor Pounce SO")]
    
    public class RaptorPounceSO : BehaviorNodeSO<RaptorContext>
    {
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new Pounce<RaptorContext>();
        }
    }
}