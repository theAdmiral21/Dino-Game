using System;
using Primitives.Checkpoints;
using Primitives.Infrastructure;

namespace Game.Core.Lifecycle
{
    public interface ICheckPointDataProvider
    {
        public CheckPointData GetCheckPointData(CheckpointId id);
        public CheckPointData GetPlayerCheckPoint(Guid PlayerId);
        public bool SetPlayerCheckpoint(Guid playerId, CheckPointData data);
        public bool SetPlayerCheckpoint(Guid playerId, CheckpointId id);
    }
}