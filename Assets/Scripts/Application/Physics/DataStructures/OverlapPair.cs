using Gameplay.Common.Core.Abstractions;
using Gameplay.Common.Application.Abstractions;
using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;

namespace Gameplay.Common.Application.DataStructures
{
    public struct OverlapPair : IOverlapPair
    {
        public IPhysicsActor Actor { get; private set; }
        public ITriggerVolume Trigger { get; private set; }

        public OverlapPair(IPhysicsActor actor, ITriggerVolume trigger)
        {
            Actor = actor;
            Trigger = trigger;
        }
    }
}