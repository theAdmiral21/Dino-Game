using System;
using System.Collections.Generic;
using Game.Core.Events;
using Game.Core.Execution;
using Game.Core.Scenes;
using Game.Core.State.Services;
using Infrastructure.Application.Abstractions;
using Infrastructure.Core.Lifecycle;
using Infrastructure.Unity.Registries;
using Infrastructure.Unity.Status;
using PlasticPipe.PlasticProtocol.Client.Proxies;
using PlayerController.Core.Config;
using PlayerController.Core.Info;
using Primitives.Characters;
using Primitives.EventBus.Abstractions;
using Primitives.Players;
using Unity.Common.Unity;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class PlayerManager : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>, ISpawnPlayer, IPlayerProvider, IDestroyPlayer
    {
        public static PlayerManager Instance { get; private set; }
        [SerializeField] private SerializedInterface<ISpawnPipeline> _spawnPipeline;
        [SerializeField] private PlayerLifeCycleHandler _lifecycleHandler;
        [SerializeField] private CharacterID _debugCharacterId;
        public HashSet<IPlayerInfo> Players => _players;
        private HashSet<IPlayerInfo> _players = new();
        public Dictionary<IPlayerInfo, GameObject> PlayerMap => _playerMap;
        private Dictionary<IPlayerInfo, GameObject> _playerMap = new();
        private IGameStateProvider _gameStateProvider;
        private IEventBus _eventBus;

        [Header("Debug")]
        private string _playerListDebug;
        private string _playerMapDebug;

        public int Priority => 0;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"INSTANTIATED PlayerManager {GetInstanceID()}");
        }
        private void OnDestroy()
        {
            Debug.Log($"DESTROYED PlayerManager {GetInstanceID()}");
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public void SpawnTestPlayer(CharacterID id)
        {
#if UNITY_EDITOR
            _debugCharacterId = id;
            // Make a debug player for testing
            Guid testId = Guid.NewGuid();
            Debug.Log($"Making test player with id: {testId} as {_debugCharacterId}");
            // Before you go spawning a new debug player. Check if you've already made one
            foreach (var player in Players)
            {
                if (player.CharacterId == id)
                {
                    SpawnPlayer(player);
                    return;
                }
            }
            PlayerInfo info = new PlayerInfo(testId, _debugCharacterId);
            SpawnPlayer(info);
#endif
        }

        // This is getting ridiculous
        public void SpawnPlayer(IPlayerInfo playerInfo, Vector2? location = null)
        {
            Debug.Log($"Info instance id (SpawnPlayer): {playerInfo.GetHashCode()}");
            // Spawn existing player
            if (_players.Contains(playerInfo) && location == null)
            {
                Debug.Log($"Spawn existing");
                _lifecycleHandler.SpawnExistingPlayer(playerInfo);
            }
            // Spawn a new player
            else if (!_players.Contains(playerInfo) && location == null)
            {
                Debug.Log($"Spawn new");
                _lifecycleHandler.SpawnNewPlayer(playerInfo);
            }
            // Spawn existing player at location
            else if (_players.Contains(playerInfo) && location != null)
            {
                Debug.Log($"Spawn existing at location");
                _lifecycleHandler.SpawnExistingPlayerAtLocation(playerInfo, (Vector2)location);
            }
            // Spawn a new player at location
            else if (!_players.Contains(playerInfo) && location != null)
            {
                Debug.Log($"Spawn new at location");
                _lifecycleHandler.SpawnNewPlayerAtLocation(playerInfo, (Vector2)location);
            }

        }

        public void Initialize(IGameContext context)
        {
            _gameStateProvider = context.GameStateServices.GameState;

        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_gameStateProvider != null, "Failed to assign game state provider");
            Debug.Assert(_eventBus != null, "Failed to assign event bus");
        }

        public void DestroyPlayer(IPlayerInfo playerInfo)
        {
            if (_playerMap.TryGetValue(playerInfo, out GameObject playerObject))
            {
                _playerMap.Remove(playerInfo);
                Destroy(playerObject);
            }
        }

        public void TrackPlayer(GameObject playerObject)
        {
            IPlayerInfo info = playerObject.GetComponentInChildren<IPlayerInfoProvider>().PlayerInfo;
            Debug.Log($"Info instance id (TrackPlayer): {info.GetHashCode()}");
            if (_playerMap.TryAdd(info, playerObject))
            {
                _players.Add(info);
                return;
            }
            Debug.LogError($"GameObject {playerObject} is already paired with a player info {info} in the player map dictionary.");
        }

        private void LateUpdate()
        {
            _playerListDebug = null;
            _playerMapDebug = null;

            foreach (var player in _players)
            {
                _playerListDebug += $"{player.PlayerId};";
            }

            foreach (var infoKey in _playerMap.Keys)
            {
                _playerMapDebug += $"{infoKey.GetHashCode()},{_playerMap[infoKey]};";
            }
        }
    }
}