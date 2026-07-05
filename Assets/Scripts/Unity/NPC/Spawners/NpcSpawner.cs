using System.Collections.Generic;
using Core.NPC.Services;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Primitives.Audio.EntityKeys;
using Unity.Equipment.DataStructures;
using Unity.Game.GameLoop;
using UnityEngine;

namespace Unity.NPC.Spawners
{
    public struct DelayedSpawn
    {
        public readonly EnemyEntityKey Prefab;
        public readonly Vector3 Location;
        public readonly Quaternion Rotation;

        public DelayedSpawn(EnemyEntityKey prefab, Vector3 location, Quaternion rotation)
        {
            Prefab = prefab;
            Location = location;
            Rotation = rotation;
        }
    }
    public class NpcSpawner : SelfRegister<IInitializable<IGameContext>>,
                               IInitializable<IGameContext>,
                               ISpawnNpcService
    {
        [SerializeField] private NpcLibrarySO _npcLibrary;
        private Dictionary<EnemyEntityKey, GameObject> _npcDict = new();

        [Header("Init Priority")]
        [SerializeField] private int _priority;
        public int Priority => _priority;

        public bool IsInitialized { get; private set; } = false;
        private IGameContext _gameContext;

        private Queue<DelayedSpawn> _delayedSpawns = new();
        private void Awake()
        {
            base.Awake();
            _npcDict = _npcLibrary.GetDict();
        }
        public void Initialize(IGameContext context)
        {
            _gameContext = context;
            Debug.Assert(_gameContext != null, $"{name} failed to initialize game context");
            IsInitialized = true;
        }

        public void PostInitialize(IGameContext context)
        {
            // Spawn the queued objects
            SpawnQueued();
        }

        public GameObject RequestNpcSpawn(EnemyEntityKey key, Vector3 location, Quaternion rotation = default)
        {
            // If we aren't ready just hold on to the request
            if (!IsInitialized)
            {
                Debug.LogWarning($"{key} was requested, but the NPC spawner is not ready!");
                _delayedSpawns.Enqueue(new DelayedSpawn
                (
                    key,
                    location,
                    rotation
                ));
                return null;
            }

            return SpawnNpc(key, location, rotation);
        }

        private void SpawnQueued()
        {
            while (_delayedSpawns.Count > 0)
            {
                DelayedSpawn delayed = _delayedSpawns.Dequeue();
                SpawnNpc(delayed.Prefab, delayed.Location, delayed.Rotation);
            }
        }

        private GameObject SpawnNpc(EnemyEntityKey key, Vector3 location, Quaternion rotation = default)
        {
            GameObject prefab = GetPrefab(key);
            GameObject instance = Instantiate(prefab, location, rotation);
            // Initialize the npc
            InitFactory.InitializeObject(_gameContext, instance);

            return instance;
        }

        private GameObject GetPrefab(EnemyEntityKey key)
        {
            return _npcDict[key];
        }
    }
}