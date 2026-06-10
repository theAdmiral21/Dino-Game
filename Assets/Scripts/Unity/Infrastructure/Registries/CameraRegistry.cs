using System.Collections.Generic;
using Game.Core.Cameras;
using UnityEngine;

namespace Infrastructure.Unity.Registries
{
    public class CameraRegistry : MonoBehaviour, ICameraRegistry
    {
        [Header("Debug")]
        [SerializeField]
        private Registry<ICameraProvider> _cameras = new();
        public IReadOnlyCollection<ICameraProvider> Cameras => _cameras.Entities;

        public void Awake()
        {
            RegistryGateway.SetRegistry(_cameras);
        }

        public void ClearRegistry()
        {
            _cameras.Clear();
        }
    }
}