using Core.Common.Abstractions;
using Game.Core.Health;
using Game.Core.Lifecycle;
using Infrastructure.Unity.DataStructures;
using Primitives.SaveData;
using Unity.Common.Unity;
using Unity.Game.SaveData;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class GetSaveData : MonoBehaviour
    {
        [SerializeField] DefaultPlayerDataSO _defaultPlayerDataSO;

        [SerializeField] private SerializedInterface<ICheckPointDataProvider> _checkpointMapperMono;
        private ICheckPointDataProvider _checkpointMapper => _checkpointMapperMono.Interface;

        public SpawnData Fetch(ref SpawnData data)
        {
            bool isFreshSpawn = IsNewSpawn();

            if (isFreshSpawn)
            {
                // Get the default data
                PlayerSaveData defaultValues = _defaultPlayerDataSO.GetPlayerDefaults();



                // Does checkpoint and position data go here or some where else? 
                // _checkpointMapper.SetPlayerCheckpoint(data.PlayerId, defaultValues.CheckpointData.Id);

                data.SpawnPoint = null;

                data.SaveData = defaultValues;
            }
            else
            {
                Debug.LogError($"Implement fetching save data");
            }

            return data;
        }

        private bool IsNewSpawn()
        {
            Debug.Log($"Implement checking for new spawns");
            return true;
        }
    }
}