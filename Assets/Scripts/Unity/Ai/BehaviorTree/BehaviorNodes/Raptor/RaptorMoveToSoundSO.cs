using AI.Core.Behavior;
using Application.Ai.BehaviorTreeNodes.Actions;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "RaptorMoveToSoundSO", menuName = "AI/Enemy/Behaviors/Raptor Move To Sound SO")]
    public class RaptorMoveToSoundSO : BehaviorNodeSO<RaptorContext>
    {
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new MoveToSound<RaptorContext>();
        }
    }
}