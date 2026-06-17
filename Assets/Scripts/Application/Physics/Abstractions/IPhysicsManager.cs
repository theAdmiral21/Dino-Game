using System.Collections.Generic;
using Core.Physics.Triggers;
using Physics.Core.PhysicsActors;

namespace Physics.Application.Abstractions
{
    public interface IPhysicsManager
    {
        public HashSet<IPhysicsActor> ActorRegistry { get; }
        public HashSet<ITriggerVolume> TriggerRegistry { get; }
    }
}