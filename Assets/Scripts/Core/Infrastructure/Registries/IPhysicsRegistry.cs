using System.Collections.Generic;
using Core.Physics.Triggers;
using Physics.Core.PhysicsActors;

namespace Infrastructure.Unity.Registries
{
    public interface IPhysicsRegistry
    {
        public IReadOnlyCollection<IPhysicsActor> Actors { get; }
        public IReadOnlyCollection<ITriggerVolume> Triggers { get; }

    }
}