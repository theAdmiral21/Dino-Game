using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "BallRunAwaySO", menuName = "AI/Enemy/Behaviors/Ball Run Away SO")]
    public class BallRunAwaySO : BehaviorNodeSO<BallContext>
    {
        [SerializeField] private float _minSafeDistance = 5;
        public override IBehaviorNode<BallContext> BuildRunTime()
        {
            return new RunAway<BallContext>(_minSafeDistance);
        }
    }
}