using System.Collections.Generic;
using Environment.Core.Level;
using Infrastructure.Unity.Registries;
using UnityEngine;

namespace Infrastructure.Unity
{
    public class SceneSpawnRegistry : MonoBehaviour, ISpawnRegistry
    {
        public IReadOnlyCollection<ISpawnPoint> SpawnPoints => _spawnPoints.Entities;
        private Registry<ISpawnPoint> _spawnPoints = new();

        public void Awake()
        {
            RegistryGateway.SetRegistry(_spawnPoints);
        }
    }
}