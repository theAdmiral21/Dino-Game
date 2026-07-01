
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "Idle", menuName = "AI/Enemy/Behaviors/Raptor/Raptor Idle SO")]

    public class RaptorIdleSO : BehaviorNodeSO<RaptorContext>
    {
        [SerializeField] private float _idleTime = 5;

        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {
            return new Idle<RaptorContext>(_idleTime);
        }
    }
}