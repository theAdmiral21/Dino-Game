
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "Bite", menuName = "AI/Enemy/Behaviors/Raptor/Raptor Bite SO")]

    public class RaptorBiteSO : BehaviorNodeSO<RaptorContext>
    {
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new BiteNode<RaptorContext>();
        }
    }
}