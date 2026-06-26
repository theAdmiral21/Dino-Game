using Core.Detection.DataStructures;

namespace Core.Ai.State.BehaviorContext
{
    public interface IPerceptionContext
    {
        public PerceptionState Perception { get; }
        public void UpdatePerception(PerceptionState state);
    }
}