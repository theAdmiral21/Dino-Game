using System.Collections.Generic;
using AI.Application.Transitions;
using AI.Core.State.Abstractions;
using AI.Unity.StateMachine.BaseClasses;
using Enemy.Application.StateContexts;
using UnityEngine;

namespace AI.Unity.StateMachine
{
    [CreateAssetMenu(fileName = "EnemyDetectTransitionSO", menuName = "AI/Enemy/Transitions/Detect Transition SO")]
    public class EnemyDetectTransitionSO : ScriptableTransition<EnemyContext>
    {
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
            return new DetectTransition<EnemyContext>(ToState, FromState);
        }
    }
}