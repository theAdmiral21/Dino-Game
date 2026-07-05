using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Unity;
using UnityEngine;
using System;
using Infrastructure.Unity.Registries;
using Game.Core.Scenes;
using Game.Core.Execution;
using Primitives.Characters;
using Infrastructure.Unity.Players;
using Primitives.Common.Scenes;
using Infrastructure.Core.Services;
using Primitives.Players;
using Unity.Game.GameLoop;

namespace Game.Unity.GameLoop
{
    /// <summary>
    /// Boot strapper used to prepare a given scene. The boot strapper uses the GameContextRegistry to initialize subscribed objects within the scene. Objects are initialized in two passes, Initialize and Post Initialize, to prevent race conditions.
    /// </summary>
    public class SceneBootStrapper : MonoBehaviour, ISceneBootStrapper
    {
        [SerializeField] GameObject _gsmPrefab;
        [SerializeField] private bool _printDebug;
        private List<IInitializable<IGameContext>> _systemList = new();

        public IReadOnlyCollection<IInitializable<IGameContext>> Systems => _systemRegistry.Systems;
        [SerializeField] private MonoBehaviour _systemRegistryMono;
        private ISceneInitRegistry _systemRegistry;

        private ISceneContextService _sceneContextService;
        private ISceneEvents _sceneEvents;

        private IGameContext _gameContext;

        [Header("Test Player Spawning")]
        public bool SpawnDebugPlayer;
        public CharacterID DebugCharacterId;

        public event Action OnReady;

        public void Awake()
        {
            if (GameSystemsManager.Instance == null)
            {
                Instantiate(_gsmPrefab);
            }
            // SceneInitializationRegistry.SetBootStrapper(this);
            GameSystemsManager.Instance.RegisterBootStrapper(this);

            _systemRegistry = _systemRegistryMono as ISceneInitRegistry;
            if (_systemRegistry == null)
            {
                Debug.LogError($"Unable to convert {_systemRegistryMono.name} to ISceneInitRegistry.");
                return;
            }

            // Debug.Log($"Scene boot strapper Awake; Frame-{Time.frameCount}");
            // Debug.Log($"SceneBootstrapper Awake in scene: {gameObject.scene.name}");
        }
        private void Start()
        {
            // Initialize everything
            RunInitialization();

            // Notify the GSM that we're ready to start
            OnReady?.Invoke();
        }

        public void RunInitialization()
        {
            // Debug.Log($"Scene boot strapper BootStrapScene; Frame-{Time.frameCount}");
            _gameContext = GameContextRegistry.GameContext;
            if (_gameContext == null)
            {
                Debug.LogError($"Game context is null. Check the GameSystemsManager.");
                return;
            }

            // Set the scene events
            _sceneEvents = _gameContext.SceneServices.SceneEvents;
            _sceneEvents.SceneChangeStarted += TearDown;
            // Set the scene context service
            _sceneContextService = _gameContext.SceneContextService;
            // Consume any persistent data
            ConsumePersistent();

            // Order initialization order by priority.
            _systemList = Systems.OrderBy(s => s.Priority).ToList();

            if (_printDebug)
            {
                PrintDebug();
            }

            InitFactory.InitializeObject(_gameContext, _systemList);
            // // Do I call this here or some where else? 
            // InitializeScene();


            // PostInitializeScene();

#if UNITY_EDITOR
            SpawnDebugPlayers();
#endif
        }

        public void SpawnDebugPlayers()
        {
            if (SpawnDebugPlayer)
            {
                PlayerManager.Instance.SpawnTestPlayer(DebugCharacterId);
            }
        }

        // public void InitializeScene()
        // {
        //     foreach (var system in _systemList)
        //     {
        //         // if (_printDebug) Debug.Log($"Initializing: {system}");

        //         system.Initialize(_gameContext);
        //     }
        // }

        // public void PostInitializeScene()
        // {
        //     foreach (var system in _systemList)
        //     {
        //         // if (_printDebug) Debug.Log($"Post initializing: {system}");

        //         system.PostInitialize(_gameContext);
        //     }
        // }
        public void ConsumePersistent()
        {

        }
        public void TearDown(SceneId _)
        {

            // Save the players overworld position
            foreach (IPlayerInfo playerInfo in PlayerManager.Instance.Players)
            {
                Debug.Log($"Player info instance id (TearDown): {playerInfo.GetHashCode()}");
                Debug.Log($"Player id: {playerInfo.PlayerId}");
                Debug.Log($"type: {playerInfo.GetType()}");
                Vector2 pos = PlayerManager.Instance.PlayerMap[(IPlayerInfo)playerInfo].transform.position;
                _sceneContextService.SetOverworldPosition(playerInfo, pos);
            }
        }

        private void PrintDebug()
        {
            StringBuilder _debugSb = new StringBuilder();
            _debugSb.AppendLine("Initialization order:");
            for (int i = 0; i < Systems.Count; i++)
            {
                string msg = $"{i + 1}. {_systemList[i]} priority: {_systemList[i].Priority}";
                _debugSb.AppendLine(msg);
            }
            Debug.Log(_debugSb);
        }
    }
}