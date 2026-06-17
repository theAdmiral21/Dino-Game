using Physics.Core.PhysicsActors;

namespace Core.Physics.CollisionCallbacks
{
    public interface ICollisionStayedEvent
    {
        public void OnCollisionStayed(IPhysicsActor actor);
    }
}