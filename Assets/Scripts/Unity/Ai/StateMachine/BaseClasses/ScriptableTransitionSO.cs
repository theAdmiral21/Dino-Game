using System.Collections.Generic;
using AI.Core.State.Abstractions;
using AI.Unity.StateMachine.BaseClasses;
using UnityEngine;

namespace AI.Unity.StateMachine
{
    public class ScriptableTransition : ScriptableObject
    {

    }

    public abstract class ScriptableTransition<T> : ScriptableTransition
    {
        public ScriptableState ToStateSO;
        public List<ScriptableState> FromStateSO;

        public abstract ITransition<T> MapToRuntime(Dictionary<ScriptableState, IState<T>> stateDict);
    }
}