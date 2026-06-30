using Core.Ai.Behavior.Visualization;

namespace AI.Core.Behavior
{
    public interface IBehaviorNode<T> : IInspectableNode
    {
        NodeResult Tick(T context);
        void Reset(T context);
    }
}