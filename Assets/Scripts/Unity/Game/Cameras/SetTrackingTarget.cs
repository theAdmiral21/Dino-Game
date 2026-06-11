using Unity.Cinemachine;
using UnityEngine;

namespace Game.Unity.Cameras
{
    public class SetTrackingTarget : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _camera;

        public void SetFollowTarget(Transform objectTransform)
        {
            _camera.Target.TrackingTarget = objectTransform;
        }
    }
}