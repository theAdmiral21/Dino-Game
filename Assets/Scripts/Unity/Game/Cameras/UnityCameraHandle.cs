using Game.Core.Cameras;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Unity.Cameras
{
    public class UnityCameraHandle : ICameraHandle
    {
        public CinemachineCamera Camera { get; }

        public UnityCameraHandle(CinemachineCamera camera)
        {
            Camera = camera;
        }
    }
}