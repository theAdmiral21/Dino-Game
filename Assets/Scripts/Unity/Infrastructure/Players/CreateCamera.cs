using Infrastructure.Unity.DataStructures;
using Unity.Cinemachine;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class CreateCamera : BaseInitFactory
    {
        [SerializeField] private GameObject _cameraPreFab;

        public override SpawnData InstantiateObject(ref SpawnData data)
        {
            var cameraObject = Instantiate(_cameraPreFab);
            cameraObject = InitializeNewObject(cameraObject);
            cameraObject.SetActive(false);
            // Set the camera's target
            var cineCamera = cameraObject.GetComponent<CinemachineCamera>();
            cineCamera.Target.TrackingTarget = data.PlayerObject.transform;
            data.CameraObject = cameraObject;
            return data;
        }
    }
}