using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "RaptorWanderSO", menuName = "AI/Enemy/Behaviors/Raptor Wander SO")]
    public class RaptorWanderSO : BehaviorNodeSO<RaptorContext>
    {
        [SerializeField] private float _wanderTime = 3;
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new Wander<RaptorContext>(_wanderTime);
        }
    }
}