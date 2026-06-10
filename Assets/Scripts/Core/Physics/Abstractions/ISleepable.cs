namespace Physics.Core.Abstractions
{
    public interface ISleepable
    {
        public bool IsAsleep { get; }

        public void Sleep();
        public void WakeUp();
    }
}