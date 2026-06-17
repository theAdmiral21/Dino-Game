using Physics.Core.PhysicsActors;

namespace Core.Physics.Collision.Callbacks
{
    public interface ICollisionExitEvent
    {
        public void OnCollisionExit(IPhysicsActor actor);
    }
}