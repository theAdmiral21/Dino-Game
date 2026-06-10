namespace Infrastructure.Core.Lifecycle.PhysicsEntities
{
    public interface IDestructible
    {
        public bool ReadyToDestroy { get; }
        public void MarkForDestruction();
        public void Destruct();
    }
}