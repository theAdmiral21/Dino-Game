using System;
using Game.Core.Lifecycle;
using Infrastructure.Unity.DataStructures;
using Unity.Common.Unity;
using Unity.Tools.DebugTools;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class SpawnResolver : MonoBehaviour
    {
        // This will need some sort of save data to determine if a check point is active

        [SerializeField] private SerializedInterface<ICheckPointDataProvider> _checkpointMapperMono;
        private ICheckPointDataProvider _checkpointMapper => _checkpointMapperMono.Interface;


        public SpawnData GetSpawnPoint(ref SpawnData data)
        {
            Debug.Log($"Use spawn at: {SpawnAt.Instance.SpawnDebugPlayer}");
            if (SpawnAt.Instance.SpawnDebugPlayer)
            {
                Debug.Log($"Spawning via SpawnAt");
                var checkpointData = _checkpointMapper.GetCheckPointData(SpawnAt.Instance.SelectedSpawn);
                data.SpawnPoint = checkpointData.Position;
                Debug.Log($"Spawning at: {checkpointData.Id}");
                return data;
            }

            Debug.Log($"Be sure to extend this with save data later.");
            if (!data.SpawnPoint.HasValue)
            {
                var checkpointData = _checkpointMapper.GetPlayerCheckPoint(data.PlayerId);
                data.SpawnPoint = checkpointData.Position;
            }
            Debug.Log($"Got spawn point: {data.SpawnPoint}");
            return data;
        }

        public Vector2 GetRespawnPoint(Guid playerId)
        {
            // Debug.Log($"Be sure to extend this with save data later.");
            // return _registry.SpawnPoints.First().Spawn;
            Debug.Log($"Got spawn point at {_checkpointMapper.GetPlayerCheckPoint(playerId).Position}");
            return _checkpointMapper.GetPlayerCheckPoint(playerId).Position;

        }
    }
}