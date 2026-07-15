using Infrastructure.Unity.DataStructures;
using Unity.Game.SaveData;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class GetSaveData : MonoBehaviour
    {
        [SerializeField] DefaultPlayerDataSO _defaultValues;
        public SpawnData Fetch(ref SpawnData data)
        {
            bool isFreshSpawn = IsNewSpawn();

            if (isFreshSpawn)
            {

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