using AI.Application.States;
using AI.Core.State.Abstractions;
using AI.Unity.StateMachine.BaseClasses;
using Enemy.Application.StateContexts;
using UnityEngine;

namespace AI.Unity.StateMachine
{
    [CreateAssetMenu(fileName = "EnemyIdleStateSO", menuName = "AI/Enemy/States/Idle State SO")]
    public class EnemyIdleStateSO : ScriptableState<EnemyContext>
    {
        public override IState<EnemyContext> BuildRunTime()
        {
            return new IdleState<EnemyContext>();
        }
    }
}