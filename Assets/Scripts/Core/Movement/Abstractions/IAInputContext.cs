using Core.Movement.Inputs;

namespace Movement.Core.Abstractions
{
    public interface IAInputContext
    {
        public IAiInput AiInput { get; }
    }
}