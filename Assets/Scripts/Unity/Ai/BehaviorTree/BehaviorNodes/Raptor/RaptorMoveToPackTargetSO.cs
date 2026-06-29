using AI.Core.Behavior;
using Application.Ai.BehaviorTreeNodes.Actions;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "RaptorMoveToPackTargetSO", menuName = "AI/Enemy/Behaviors/Raptor Move to Pack Target SO")]
    public class RaptorMoveToPackTargetSO : BehaviorNodeSO<RaptorContext>
    {
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new MoveToPackTarget<RaptorContext>();
        }
    }
}