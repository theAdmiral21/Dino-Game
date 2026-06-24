using AI.Application.States;
using AI.Core.State.Abstractions;
using AI.Unity.StateMachine.BaseClasses;
using Enemy.Application.StateContexts;
using UnityEngine;

namespace AI.Unity.StateMachine
{
    [CreateAssetMenu(fileName = "EnemyDeadStateSO", menuName = "AI/Enemy/States/Dead State SO")]
    public class EnemyDeadStateSO : ScriptableState<EnemyContext>
    {
        public override IState<EnemyContext> BuildRunTime()
        {
            return new DeadState<EnemyContext>();
        }
    }
}