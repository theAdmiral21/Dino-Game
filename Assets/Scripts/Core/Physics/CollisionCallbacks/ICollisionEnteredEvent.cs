using Physics.Core.PhysicsActors;

namespace Core.Physics.CollisionCallbacks
{
    public interface ICollisionEnteredEvent
    {
        public void OnCollisionEntered(IPhysicsActor actor);
    }
}