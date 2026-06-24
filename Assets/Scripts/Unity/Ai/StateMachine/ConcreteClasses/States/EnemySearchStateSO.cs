using AI.Application.States;
using AI.Core.State.Abstractions;
using AI.Unity.StateMachine.BaseClasses;
using Enemy.Application.StateContexts;
using UnityEngine;

namespace AI.Unity.StateMachine
{
    [CreateAssetMenu(fileName = "EnemySearchStateSO", menuName = "AI/Enemy/States/Search State SO")]
    public class EnemySearchStateSO : ScriptableState<EnemyContext>
    {
        public override IState<EnemyContext> BuildRunTime()
        {
            return new SearchState<EnemyContext>();
        }
    }
}