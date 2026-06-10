namespace Movement.Core.Stats
{
    public interface IStatCollection
    {
        public T Get<T>();

        public bool TryGet<T>(out T stat);
    }
}