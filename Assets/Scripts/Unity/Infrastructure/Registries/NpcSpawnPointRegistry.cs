using System.Collections.Generic;
using Core.NPC;
using UnityEngine;

namespace Infrastructure.Unity.Registries
{
    public class NpcSpawnPointRegistry : MonoBehaviour, INpcSpawnPointRegistry
    {
        [Header("Debug")]
        [SerializeField]
        private Registry<INpcSpawnPoint> _spawnPoints = new();
        public IReadOnlyCollection<INpcSpawnPoint> SpawnPoints => _spawnPoints.Entities;

        public void Awake()
        {
            RegistryGateway.SetRegistry(_spawnPoints);
        }
    }
}