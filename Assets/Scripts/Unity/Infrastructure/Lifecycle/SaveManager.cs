using Core.Game.Lifecycle;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Infrastructure.Lifecycle
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private SerializedInterface<ISaveRegistry> _saveRegistryMono;
        private ISaveRegistry _saveRegistry => _saveRegistryMono.Interface;

        public static SaveManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"INSTANTIATED SaveManager {GetEntityId()}");
        }
        private void OnDestroy()
        {
            Debug.Log($"DESTROYED SaveManager {GetEntityId()}");
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}