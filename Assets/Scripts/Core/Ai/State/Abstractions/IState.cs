using AI.Core.State.Enums;

namespace AI.Core.State.Abstractions
{
    public interface IState<T>
    {
        public void Enter(T context);
        public void Update(T context);
        public void Exit(T context);
    }

}