using Core.Inventory;
using Infrastructure.Unity.DataStructures;
using Physics.Core.PhysicsActors;
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

            // While you're here setup the inventory presenter
            var presenter = cameraObject.GetComponentInChildren<IInventoryPresenter>();
            var playerInventory = data.PlayerObject.GetComponentInChildren<IInventory>();
            presenter.SetEventBus(playerInventory.InventoryEventBus);

            return data;
        }
    }
}