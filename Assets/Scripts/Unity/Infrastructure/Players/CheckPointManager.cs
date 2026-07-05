using System;
using System.Collections.Generic;
using Environment.Core.Abstractions;
using Environment.Core.Level;
using Game.Core.Events;
using Game.Core.Execution;
using Game.Core.Lifecycle;
using Infrastructure.Unity.Registries;
using Primitives.EventBus.Abstractions;
using Primitives.Infrastructure;
using Unity.Common.Unity;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class CheckPointManager : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>, ICheckPointDataProvider
    {
        [SerializeField] private SerializedInterface<ICheckpointRegistry> _checkpoints;
        public IReadOnlyCollection<ICheckpoint> Checkpoints => _checkpoints.Interface.Checkpoints;
        private IEventBus _eventBus;
        private Dictionary<Guid, CheckPointData> _checkpointMap = new();
        [SerializeField] private int _priority = 0;
        public int Priority => _priority;
        public void Initialize(IGameContext context)
        {
            _eventBus = context.EventBus;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_eventBus != null, "Failed to assign event bus");

            _eventBus.Subscribe<CheckPointTriggeredEvent>(HandleCheckPointTrigger);
        }

        private void HandleCheckPointTrigger(CheckPointTriggeredEvent checkPointTriggered)
        {
            // Update the checkpoint for the player that triggered it
            _checkpointMap[checkPointTriggered.PlayerId] = checkPointTriggered.Data;
        }

        public CheckPointData GetCheckPoint(Guid PlayerId)
        {
            if (_checkpointMap.TryGetValue(PlayerId, out CheckPointData data))
            {
                return data;
            }
#if UNITY_EDITOR
            var checkpoint = GetDebugStart();
#else
            var checkpoint = GetLevelStart();
#endif
            if (checkpoint != null)
            {
                return (CheckPointData)checkpoint;
            }
            throw new KeyNotFoundException("Could not find level start checkpoint.");
        }

        public bool SetCheckpoint(Guid playerId, CheckPointData data)
        {
            return _checkpointMap.TryAdd(playerId, data);
        }

        private CheckPointData? GetLevelStart()
        {
            Debug.Log($"Fetching level start");
            foreach (var checkpoint in Checkpoints)
            {
                if (checkpoint.IsLevelStart)
                {
                    return checkpoint.Data;
                }
            }
            return null;
        }

        private CheckPointData? GetDebugStart()
        {
            Debug.Log($"Fetching debug start");
            foreach (var checkpoint in Checkpoints)
            {
                if (checkpoint.IsDebugStart)
                {
                    return checkpoint.Data;
                }
            }
            return null;
        }
    }
}