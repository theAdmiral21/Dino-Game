using Core.Detection;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Detection.DetectionManager.cs
{
    public class DetectionManager : MonoBehaviour, IDetectionManager
    {
        public IDetectionRegistry Detectors => _detectors.Interface;
        [SerializeField] private SerializedInterface<IDetectionRegistry> _detectors;

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
            Debug.Log($"INSTANTIATED DetectionManager {GetInstanceID()}");
        }

        private void OnDestroy()
        {
            Debug.Log($"DESTROYED DetectionManager {GetInstanceID()}");
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}