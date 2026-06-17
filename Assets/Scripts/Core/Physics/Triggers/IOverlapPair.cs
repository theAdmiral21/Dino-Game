using Physics.Core.PhysicsActors;

namespace Core.Physics.Triggers
{
    public interface IOverlapPair
    {
        public IPhysicsActor Actor { get; }
        public ITriggerVolume Trigger { get; }
    }
}