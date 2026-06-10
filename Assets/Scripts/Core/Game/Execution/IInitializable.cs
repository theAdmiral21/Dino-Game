namespace Game.Core.Execution
{
    // NOTE Think about making this into an abstract base class that extends monobehaviours so you can implement the awake method to register the methods properly.
    public interface IInitializable<T>
    {
        public int Priority { get; }
        public void Initialize(T context);

        public void PostInitialize(T context);
    }
}