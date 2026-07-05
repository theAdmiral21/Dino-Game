using Core.Movement.Inputs;

namespace Movement.Core.Abstractions
{
    public interface IActorInputContext
    {
        public IActorInput ActorInput { get; }
    }
}