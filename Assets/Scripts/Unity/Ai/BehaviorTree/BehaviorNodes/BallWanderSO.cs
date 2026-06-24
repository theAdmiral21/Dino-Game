using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace AI.Unity.BehaviorTree
{
    [CreateAssetMenu(fileName = "BallWanderSO", menuName = "AI/Enemy/Behaviors/Ball Wander SO")]
    public class BallWanderSO : BehaviorNodeSO<BallContext>
    {
        [SerializeField] private float _wanderTime = 3;
        public override IBehaviorNode<BallContext> BuildRunTime()
        {
            return new Wander<BallContext>(_wanderTime);
        }
    }
}