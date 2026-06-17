using Physics.Core.PhysicsActors;
using Core.Physics.Triggers;

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