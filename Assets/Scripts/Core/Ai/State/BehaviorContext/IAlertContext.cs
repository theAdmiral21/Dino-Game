using Core.Ai.BlackBoard;

namespace Core.Ai.State.BehaviorContext
{
    public interface IAlertContext
    {
        public AlertLevel Alertness { get; }
        public void SetAlertLevel(AlertLevel alertness);
    }
}