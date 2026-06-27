using Core.Ai.BlackBoard;

namespace Core.Ai.State.BehaviorContext
{
    public interface IStatusContext
    {
        public Status CurrentStatus { get; }
        public void SetStatus(Status status);
    }
}