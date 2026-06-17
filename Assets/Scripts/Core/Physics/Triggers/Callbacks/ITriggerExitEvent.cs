using Physics.Core.PhysicsActors;
namespace Core.Physics.Triggers.Callbacks
{
    public interface ITriggerExitEvent
    {
        public void OnTriggerExited(IPhysicsActor actor);

    }
}