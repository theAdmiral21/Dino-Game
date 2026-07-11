using AI.Core.Behavior;
using Application.Ai.BehaviorTreeNodes.Actions;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "RaptorChaseSO", menuName = "AI/Enemy/Behaviors/Raptor Chase SO")]
    public class RaptorChaseSO : BehaviorNodeSO<RaptorContext>
    {
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new Chase<RaptorContext>();
        }
    }
}