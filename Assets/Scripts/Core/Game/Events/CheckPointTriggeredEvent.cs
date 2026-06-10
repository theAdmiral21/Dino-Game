using System;
using Primitives.Infrastructure;

namespace Game.Core.Events
{
    public record CheckPointTriggeredEvent
    {
        public CheckPointData Data;
        public Guid PlayerId;

        public CheckPointTriggeredEvent(CheckPointData data, Guid playerId)
        {
            Data = data;
            PlayerId = playerId;
        }
    }
}