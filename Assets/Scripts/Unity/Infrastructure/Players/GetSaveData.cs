using Core.Common.Abstractions;
using Game.Core.Health;
using Infrastructure.Unity.DataStructures;
using Primitives.SaveData;
using Unity.Game.SaveData;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class GetSaveData : MonoBehaviour
    {
        [SerializeField] DefaultPlayerDataSO _defaultPlayerDataSO;
        public SpawnData Fetch(ref SpawnData data)
        {
            bool isFreshSpawn = IsNewSpawn();

            if (isFreshSpawn)
            {
                // Get the default data
                PlayerSaveData defaultValues = _defaultPlayerDataSO.GetPlayerDefaults();

                // or lord do I go through and find everything that needs pieces of player data? Or do I add a method to the save orchestrator to sort it out? 

                // Find the data, the save orchestrator is busy anyways
                data.PlayerObject.GetComponentInChildren<IInitObject<HealthSaveData>>().Init(defaultValues.HealthData);

                data.PlayerObject.GetComponentInChildren<IInitObject<InventorySaveData>>().Init(defaultValues.InventoryData);

                data.PlayerObject.GetComponentInChildren<IInitObject<EquipmentSaveData>>().Init(defaultValues.EquipmentData);


                // Does checkpoint and position data go here or some where else? 
                // data.PlayerObject.GetComponent<IInitObject<HealthSaveData>>().Init(defaultValues.HealthData);

                // data.PlayerObject.GetComponent<IInitObject<HealthSaveData>>().Init(defaultValues.HealthData);
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