using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "IsPlayerNearBallSO", menuName = "AI/Enemy/Behaviors/Is Player Near Ball SO")]
    public class IsPlayerNearBallSO : BehaviorNodeSO<BallContext>
    {
        [SerializeField] private float _minSafeDistance = 5;
        public override IBehaviorNode<BallContext> BuildRunTime()
        {
            return new IsPlayerNear<BallContext>(_minSafeDistance);
        }
    }
}