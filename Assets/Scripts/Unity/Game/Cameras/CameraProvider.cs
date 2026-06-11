using Game.Core.Cameras;
using Infrastructure.Unity.Registries;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Unity.Cameras
{
    public class CameraProvider : SelfRegister<ICameraProvider>, ICameraProvider
    {

        [SerializeField] private CinemachineCamera _camera;
        private ICameraHandle _handle;

        public ICameraHandle GetCamera() => _handle;

        private new void Awake()
        {
            base.Awake();
            _handle = new UnityCameraHandle(_camera);
        }
    }
}