using Core.Movement.Inputs;

namespace Movement.Core.Abstractions
{
    public interface IInputContext
    {
        public IAiInput AiInput { get; }
    }
}