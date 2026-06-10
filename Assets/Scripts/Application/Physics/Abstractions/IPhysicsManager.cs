using System.Collections.Generic;
using Gameplay.Common.Application.Abstractions;
using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;

namespace Physics.Application.Abstractions
{
    public interface IPhysicsManager
    {
        public HashSet<IPhysicsActor> ActorRegistry { get; }
        public HashSet<ITriggerVolume> TriggerRegistry { get; }
    }
}