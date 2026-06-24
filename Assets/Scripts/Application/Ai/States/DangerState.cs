using AI.Core.State.Abstractions;
using AI.Core.State.Enums;
using Primitives.Damage;

namespace AI.Application.States
{
    public class DangerState<T> : IState<T> where T : IDamageDealer
    {
        public StateType State => StateType.Idle;

        public void Enter(T context)
        {

        }

        public void Update(T context)
        {

        }

        public void Exit(T context)
        {

        }
    }
}