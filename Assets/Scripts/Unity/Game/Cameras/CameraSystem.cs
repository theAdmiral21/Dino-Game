using Game.Core.Cameras;
using Unity.Cinemachine;
using Unity.Common.Unity;
using UnityEngine;

namespace Game.Unity.Cameras
{
    public class CameraSystem : MonoBehaviour
    {
        private enum CamPriority
        {
            Active,
            InActive,
        }
        [SerializeField] private SerializedInterface<ICameraManager> _cameraManagerMono;
        private ICameraManager _cameraManager => _cameraManagerMono.Interface;

        private CinemachineCamera _currentCamera;

        public void AddCamera(ICameraHandle handle)
        {
            if (handle != null)
            {
                var unityHandle = handle as UnityCameraHandle;

                // Debug.Log($"Got camera: {unityHandle.Camera.gameObject.name}");

                if (unityHandle == null) return;

                SetPriority(unityHandle.Camera, CamPriority.Active);
            }
        }

        public void RemoveCamera(ICameraHandle handle)
        {
            if (handle != null)
            {
                var unityHandle = handle as UnityCameraHandle;

                // Debug.Log($"Got camera: {unityHandle.Camera.gameObject.name}");

                if (unityHandle == null) return;

                SetPriority(unityHandle.Camera, CamPriority.InActive);
            }
        }


        private void SetPriority(CinemachineCamera camera, CamPriority priority)
        {
            if (priority == CamPriority.Active)
            {
                camera.Priority = 2;
            }
            else
            {
                camera.Priority = 0;
            }
        }
    }
}