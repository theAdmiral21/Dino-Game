using System.Collections.Generic;
using Gameplay.Common.Application.Abstractions;
using Infrastructure.Core.Registries;
using Physics.Core.PhysicsActors;

namespace Infrastructure.Unity.Registries
{
    public interface IPhysicsRegistry
    {
        public IReadOnlyCollection<IPhysicsActor> Actors { get; }
        public IReadOnlyCollection<ITriggerVolume> Triggers { get; }

    }
}