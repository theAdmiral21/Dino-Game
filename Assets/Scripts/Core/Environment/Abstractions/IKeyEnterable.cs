namespace Core.Environment.Abstractions
{
    public interface IKeyEnterable
    {
        public bool EnterValue(int val);
        public void RemoveLast();
    }
}