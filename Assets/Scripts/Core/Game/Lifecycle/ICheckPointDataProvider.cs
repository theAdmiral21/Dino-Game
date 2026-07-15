using System;
using Primitives.Checkpoints;
using Primitives.Infrastructure;

namespace Game.Core.Lifecycle
{
    public interface ICheckPointDataProvider
    {
        public CheckPointData GetCheckPoint(Guid PlayerId);
        public bool SetCheckpoint(Guid playerId, CheckPointData data);
        public bool SetCheckpoint(Guid playerId, CheckpointId id);
    }
}