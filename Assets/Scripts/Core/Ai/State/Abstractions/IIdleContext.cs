namespace AI.Core.State.Abstractions
{
    public interface IIdleContext
    {
        public void StopMoving();
        public void UpdateIdleContext();
    }
}