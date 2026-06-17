using Physics.Core.PhysicsActors;
namespace Core.Physics.Triggers.Callbacks
{
    public interface ITriggerEnterEvent
    {
        public void OnTriggerEntered(IPhysicsActor actor);
    }
}