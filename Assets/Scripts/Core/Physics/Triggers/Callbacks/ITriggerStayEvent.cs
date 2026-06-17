using Physics.Core.PhysicsActors;
namespace Core.Physics.Triggers.Callbacks
{
    public interface ITriggerStayEvent
    {
        public void OnTriggerStayed(IPhysicsActor actor);
    }
}