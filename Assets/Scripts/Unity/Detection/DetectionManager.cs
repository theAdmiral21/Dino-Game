using Codice.Utils;
using Core.Detection;
using Core.Detection.Olfactory;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Detection.DetectionManager.cs
{
    public class DetectionManager : MonoBehaviour, IDetectionManager
    {
        // Why do I have these?
        // public IDetectionRegistry Detectors => _detectors.Interface;
        // [SerializeField] private SerializedInterface<IDetectionRegistry> _detectors;
        public IScentMap ScentMap { get; private set; }

        public static DetectionManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            DontDestroyOnLoad(gameObject);
            Debug.Log($"INSTANTIATED DetectionManager {GetEntityId()}");

            ConfigManager();
        }

        private void OnDestroy()
        {
            Debug.Log($"DESTROYED DetectionManager {GetEntityId()}");
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void ConfigManager()
        {
            ScentMap = new ScentMap();
        }

        private void FixedUpdate()
        {
            ScentMap.Decay(Time.fixedDeltaTime);
        }
    }
}