using System.Collections.Generic;
using AI.Application.Transitions;
using AI.Core.State.Abstractions;
using AI.Unity.StateMachine.BaseClasses;
using Enemy.Application.StateContexts;
using UnityEngine;

namespace AI.Unity.StateMachine
{
    [CreateAssetMenu(fileName = "EnemyIdleStateSO", menuName = "AI/Enemy/Transitions/Wait Transition SO")]
    public class EnemyWaitTransitionSO : ScriptableTransition<EnemyContext>
    {
        public float WaitTime;
        public IState<EnemyContext> ToState;
        public HashSet<IState<EnemyContext>> FromState = new();

        public override ITransition<EnemyContext> MapToRuntime(Dictionary<ScriptableState, IState<EnemyContext>> stateDict)
        {
            ToState = stateDict[ToStateSO];
            foreach (ScriptableState stateSO in FromStateSO)
            {
                if (stateDict.ContainsKey(stateSO))
                {
                    FromState.Add(stateDict[stateSO]);
                }
            }
            return new WaitTransition<EnemyContext>(ToState, FromState, WaitTime);
        }
    }
}