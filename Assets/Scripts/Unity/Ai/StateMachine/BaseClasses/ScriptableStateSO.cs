using System.Runtime.CompilerServices;
using AI.Core.State.Abstractions;
using UnityEngine;

namespace AI.Unity.StateMachine.BaseClasses
{
    public abstract class ScriptableState : ScriptableObject
    {

    }

    public abstract class ScriptableState<T> : ScriptableState
    {
        public abstract IState<T> BuildRunTime();
    }
}