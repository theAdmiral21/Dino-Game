namespace AI.Core.Behavior
{
    public interface IBehaviorNode<T>
    {
        NodeResult Tick(T context);

        void Reset(T context);
    }
}