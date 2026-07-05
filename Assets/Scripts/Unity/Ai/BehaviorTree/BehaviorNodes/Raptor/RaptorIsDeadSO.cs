
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "IsDead", menuName = "AI/Enemy/Behaviors/Raptor/Raptor IsDead SO")]
    
    public class RaptorIsDeadSO : BehaviorNodeSO<RaptorContext>
    {
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new IsDead<RaptorContext>();
        }
    }
}