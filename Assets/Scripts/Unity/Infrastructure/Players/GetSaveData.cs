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
            Debug.Log($"Implement fetching save data");
            return data;
        }
    }
}