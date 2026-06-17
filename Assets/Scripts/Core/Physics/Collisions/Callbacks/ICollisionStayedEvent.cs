using Physics.Core.PhysicsActors;

namespace Core.Physics.Collision.Callbacks
{
    public interface ICollisionStayedEvent
    {
        public void OnCollisionStayed(IPhysicsActor actor);
    }
}