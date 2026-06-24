using System.Collections.Generic;

namespace AI.Core.State.Abstractions
{
    public interface ITransition<T>
    {
        public IState<T> ToState { get; }
        public HashSet<IState<T>> FromStates { get; }
        public bool EvaluateTransition(T context);
    }
}