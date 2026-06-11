using Infrastructure.Core.Lifecycle.PhysicsEntities;

namespace Infrastructure.Application.Abstractions
{
    public interface IEntityDestroyer
    {
        public void AddToDestroyQueue(IDestructible destructible);
    }
}