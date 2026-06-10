using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;

namespace Gameplay.Common.Core.Abstractions
{
    public interface ITriggerEnterEvent
    {
        public void OnTriggerEntered(IPhysicsActor actor);
    }
}