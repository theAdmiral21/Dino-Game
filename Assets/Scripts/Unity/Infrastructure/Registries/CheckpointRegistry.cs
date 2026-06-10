using System.Collections.Generic;
using Environment.Core.Abstractions;
using Environment.Core.Level;
using UnityEngine;

namespace Infrastructure.Unity.Registries
{
    public class CheckpointRegistry : MonoBehaviour, ICheckpointRegistry
    {
        [Header("Debug")]
        [SerializeField]
        private Registry<ICheckpoint> _checkpoints = new();
        public IReadOnlyCollection<ICheckpoint> Checkpoints => _checkpoints.Entities;

        public void Awake()
        {
            RegistryGateway.SetRegistry(_checkpoints);
        }
    }
}