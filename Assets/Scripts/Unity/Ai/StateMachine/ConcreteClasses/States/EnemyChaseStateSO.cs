using AI.Application.States;
using AI.Core.State.Abstractions;
using AI.Unity.StateMachine.BaseClasses;
using Enemy.Application.StateContexts;
using UnityEngine;

namespace AI.Unity.StateMachine
{
    [CreateAssetMenu(fileName = "EnemyChaseStateSO", menuName = "AI/Enemy/States/Chase State SO")]
    public class EnemyChaseStateSO : ScriptableState<EnemyContext>
    {
        public override IState<EnemyContext> BuildRunTime()
        {
            return new ChaseState<EnemyContext>();
        }
    }
}