using DG.Tweening;
using Game.Core.Cameras;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Unity.Cinemachine;
using Unity.Common.Unity;
using UnityEngine;

namespace Game.Unity.Cameras
{
    public class SwapLeftRight : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        [SerializeField] private SerializedInterface<ICameraProvider> _leftCameraProviderMono;
        private ICameraProvider _leftCameraProvider => _leftCameraProviderMono.Interface;
        [SerializeField] private SerializedInterface<ICameraProvider> _rightCameraProviderMono;
        private ICameraProvider _rightCameraProvider => _rightCameraProviderMono.Interface;

        private CinemachineCamera _leftCamera;
        private CinemachineCamera _rightCamera;

        private IActiveCameraChanger _cameraChanger;

        public Transform TargetTransform { get; private set; }

        public int Priority => 0;

        private void Awake()
        {
            var temp = _leftCameraProvider.GetCamera() as UnityCameraHandle;
            _leftCamera = temp.Camera;

            temp = _rightCameraProvider.GetCamera() as UnityCameraHandle;
            _rightCamera = temp.Camera;

            Debug.Assert(_leftCamera != null, "Unable to get left camera");
            Debug.Assert(_rightCamera != null, "Unable to get right camera");
        }

        public void SetTargetTransform(Transform targetTransform)
        {
            TargetTransform = targetTransform;
            _leftCamera.Target.TrackingTarget = targetTransform;
            _rightCamera.Target.TrackingTarget = targetTransform;
        }

        private void LateUpdate()
        {
            if (TargetTransform != null)
            {
                if (TargetTransform.localScale.x < 0)
                {
                    _cameraChanger.AddCamera(_leftCameraProvider.GetCamera());
                    _cameraChanger.RemoveCamera(_rightCameraProvider.GetCamera());
                }
                else
                {
                    _cameraChanger.AddCamera(_rightCameraProvider.GetCamera());
                    _cameraChanger.RemoveCamera(_leftCameraProvider.GetCamera());
                }
            }
        }

        public void Initialize(IGameContext context)
        {
            _cameraChanger = context.CameraService.CameraChanger;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_cameraChanger != null, $"Unable to assign camera changer");
        }
    }
}