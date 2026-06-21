using Physics.Core.DataStructures;

namespace Core.Physics.Collision.Callbacks
{
    public interface ICollisionEnterEvent
    {
        public void OnCollisionEntered(CollisionInfo actor);
    }
}