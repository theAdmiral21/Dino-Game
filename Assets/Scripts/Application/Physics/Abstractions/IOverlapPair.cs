using Gameplay.Common.Application.Abstractions;
using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;

namespace Gameplay.Common.Core.Abstractions
{
    public interface IOverlapPair
    {
        public IPhysicsActor Actor { get; }
        public ITriggerVolume Trigger { get; }
    }
}