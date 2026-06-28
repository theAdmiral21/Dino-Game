using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "RaptorRunAwaySO", menuName = "AI/Enemy/Behaviors/Raptor Run Away SO")]
    public class BallRunAwaySO : BehaviorNodeSO<RaptorContext>
    {
        [SerializeField] private float _minSafeDistance = 5;
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new RunAway<RaptorContext>(_minSafeDistance);
        }
    }
}