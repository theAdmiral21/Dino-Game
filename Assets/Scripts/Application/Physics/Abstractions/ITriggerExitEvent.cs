using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;
namespace Gameplay.Common.Core.Abstractions
{
    public interface ITriggerExitEvent
    {
        public void OnTriggerExited(IPhysicsActor actor);

    }
}