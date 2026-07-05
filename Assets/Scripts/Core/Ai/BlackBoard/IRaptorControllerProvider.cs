using Core.Ai.BlackBoard;

namespace Core.Ai.BlackBoard
{
    public interface IRaptorControllerProvider
    {
        public IRaptorController RaptorController { get; }
    }
}