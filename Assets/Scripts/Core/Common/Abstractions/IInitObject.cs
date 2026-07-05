namespace Core.Common.Abstractions
{
    public interface IInitObject<T>
    {
        public void Init(T val);
    }
}