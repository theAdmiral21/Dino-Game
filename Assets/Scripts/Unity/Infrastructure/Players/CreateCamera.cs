using Core.Inventory;
using Infrastructure.Unity.DataStructures;
using Unity.Cinemachine;
using Unity.Game.UI.Hud;
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
            var presenter = cameraObject.GetComponentInChildren<IHudPresenter>();
            var playerInventory = data.PlayerObject.GetComponentInChildren<IInventory>();
            presenter.Init(playerInventory.EventBus);

            return data;
        }
    }
}