using Unity.NPC.Spawners;
using UnityEngine;

namespace Unity.NPC
{
    public class NpcManager : MonoBehaviour
    {
        [SerializeField] private NpcSpawner _spawner;
        public NpcSpawner Spawner => _spawner;

        public static NpcManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"INSTANTIATED NpcManager {GetEntityId()}");


        }
        private void OnDestroy()
        {
            Debug.Log($"DESTROYED NpcManager {GetEntityId()}");
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}