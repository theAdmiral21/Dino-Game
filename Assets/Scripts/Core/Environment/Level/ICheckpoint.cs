using Physics.Core.PhysicsActors;
using Primitives.Checkpoints;
using Primitives.Infrastructure;

namespace Environment.Core.Level
{
    public interface ICheckpoint
    {
        public CheckpointId Id { get; }
        public CheckPointData Data { get; }
        public void UpdateCheckPoint(IPhysicsActor actor);
    }
}