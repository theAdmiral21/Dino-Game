namespace Core.Equipment
{
    public interface IInitStats<T>
    {
        public void Init(T stats);
    }
}