using Infrastructure.Unity.DataStructures;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class GetSaveData : MonoBehaviour
    {
        public SpawnData Fetch(ref SpawnData data)
        {
            Debug.Log($"Implement fetching save data");
            return data;
        }
    }
}