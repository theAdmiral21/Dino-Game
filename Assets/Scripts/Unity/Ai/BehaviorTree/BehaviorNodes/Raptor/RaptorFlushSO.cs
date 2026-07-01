
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "Flush", menuName = "AI/Enemy/Behaviors/Raptor/Raptor Flush SO")]
    
    public class RaptorFlushSO : BehaviorNodeSO<RaptorContext>
    {
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new Flush<RaptorContext>();
        }
    }
}