using Physics.Core.PhysicsActors;

namespace Core.Physics.Collision.Callbacks
{
    public interface ICollisionEnterEvent
    {
        public void OnCollisionEntered(IPhysicsActor actor);
    }
}