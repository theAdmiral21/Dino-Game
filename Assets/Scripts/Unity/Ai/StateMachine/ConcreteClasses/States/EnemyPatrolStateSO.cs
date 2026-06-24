using AI.Application.States;
using AI.Core.State.Abstractions;
using AI.Unity.StateMachine.BaseClasses;
using Enemy.Application.StateContexts;
using UnityEngine;

namespace AI.Unity.StateMachine
{
    [CreateAssetMenu(fileName = "EnemyPatrolStateSO", menuName = "AI/Enemy/States/Patrol State SO")]
    public class EnemyPatrolStateSO : ScriptableState<EnemyContext>
    {
        public override IState<EnemyContext> BuildRunTime()
        {
            return new MoveToState<EnemyContext>();
        }
    }
}