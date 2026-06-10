using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;
namespace Gameplay.Common.Core.Abstractions
{
    public interface ITriggerStayEvent
    {
        public void OnTriggerStayed(IPhysicsActor actor);
    }
}