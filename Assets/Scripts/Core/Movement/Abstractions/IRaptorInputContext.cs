using Core.Movement.Inputs;

namespace Movement.Core.Abstractions
{
    public interface IRaptorInputContext
    {
        public IRaptorInput RaptorInput { get; }
    }
}