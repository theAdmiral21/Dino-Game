using System.Collections.Generic;
using System.Linq;
using Game.Core.Cameras;
using Infrastructure.Unity.Registries;
using Unity.Common.Unity;
using UnityEngine;

namespace Game.Unity.Cameras
{
    public class CameraManager : MonoBehaviour, ICameraManager, IActiveCameraChanger
    {
        [SerializeField] private SerializedInterface<ICameraRegistry> _cameraRegistryMono;
        private ICameraRegistry _cameraRegistry => _cameraRegistryMono.Interface;
        public IReadOnlyCollection<ICameraProvider> CameraRegistry => _cameraRegistry.Cameras;

        private List<ICameraHandle> _activeCameras = new();
        [SerializeField] private CameraSystem _cameraSystem;
        public static CameraManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            DontDestroyOnLoad(gameObject);
            Debug.Log($"INSTANTIATED CameraManager {GetInstanceID()}");
        }

        private void OnDestroy()
        {
            Debug.Log($"DESTROYED CameraManager {GetInstanceID()}");
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public void AddCamera(ICameraHandle handle)
        {
            // Debug.Log($"Added new camera");
            _cameraSystem.AddCamera(handle);
            // _activeCameras.Add(handle);
        }
        public void RemoveCamera(ICameraHandle handle)
        {
            // Debug.Log($"Removed camera");
            _cameraSystem.RemoveCamera(handle);
            // _activeCameras.Remove(handle);
        }

        public ICameraHandle GetActiveCamera()
        {
            return _activeCameras.LastOrDefault();
        }
    }
}