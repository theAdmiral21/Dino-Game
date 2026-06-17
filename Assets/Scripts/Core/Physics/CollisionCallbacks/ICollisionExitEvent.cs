using Physics.Core.PhysicsActors;

namespace Core.Physics.CollisionCallbacks
{
    public interface ICollisionExitEvent
    {
        public void OnCollisionExit(IPhysicsActor actor);
    }
}