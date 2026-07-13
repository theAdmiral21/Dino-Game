using Physics.Core.PhysicsActors;
using Primitives.Infrastructure;

namespace Environment.Core.Level
{
    public interface ICheckpoint
    {
        public CheckPointData Data { get; }
        public void UpdateCheckPoint(IPhysicsActor actor);
    }
}