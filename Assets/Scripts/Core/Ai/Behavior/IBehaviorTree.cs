namespace AI.Core.Behavior
{
    public interface IBehaviorTree<T>
    {
        public void Tick(T context);
        public IBehaviorNode<T> Root { get; }
    }
}