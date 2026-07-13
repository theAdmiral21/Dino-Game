using UnityEngine;
using Infrastructure.Unity;
using Infrastructure.Application.state.Services;
using Infrastructure.Application;
using Primitives.Common.Infrastructure;
using Game.Application.State;
using Game.Application.State.Services;
using Game.Application.Scenes.Services;
using Game.Application.Scenes;
using Game.Unity.Audio.Services;
using Game.Unity.Audio;
using UnityEngine.SceneManagement;
using Infrastructure.Application.EventBus;
using Primitives.EventBus.Abstractions;
using Game.Core.Scenes;
using Primitives.GameState;
using Game.Core.Execution;
using Game.Core.Audio;
using Game.Core.State.Services;
using Game.Application.DataStructures;
using Physics.Core.Services;
using Physics.Application.Services;
using Physics.Application.Abstractions;
using Infrastructure.Core.Services;
using Infrastructure.Application.Services;
using Infrastructure.Unity.Players;
using Game.Application.Cameras;
using Game.Unity.Cameras;
using Game.Core.Cameras;
using Physics.Unity.Physics;
using Core.Detection.Services;
using Application.Detection.Services;
using Core.Detection.Olfactory;
using Unity.Detection.DetectionManager.cs;
using Core.NPC.Services;
using Unity.NPC.Spawners;
using Primitives.Common.Scenes;

namespace Game.Unity
{
    /// <summary>
    /// Central orchestrator for all scene systems. This object persists throughout the life of a play session. The GSM instantiates the services contained within GameContext that are then distributed to objects in the scene via scene boot strapper. The GSM also instantiates the SimManager and GameStateManager.
    /// </summary>
    public class GameSystemsManager : MonoBehaviour
    {
        public static GameSystemsManager Instance { get; private set; }
        private ISceneBootStrapper _bootStrapper;

        [Header("Game State Context")]
        [SerializeField] private GameState _currentGameState;
        [Header("Debug")]
        [SerializeField] private bool _debugEvents = false;
        // Managers
        public GameStateManager GetGameStateManager => _gameStateManager;
        private GameStateManager _gameStateManager;

        private PhysicsManager _physicsManager;

        private IGameStateServices _gameStateServices;
        private IQuitExecutor _executeQuitService;
        private InfrastructureServices _quitServices;
        private ISceneServices _sceneServices;
        private IAudioService _audioService;
        private IEventBus _eventBus;
        private IPhysicsServices _physicsServices;
        private ICameraService _cameraService;
        private IPlayerServices _playerServices;
        private ISceneContextService _sceneContextService;
        private IDetectionServices _detectionServices;
        private ISpawnNpcService _spawnNpcService;
        private IGameContext _gameContext;


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _physicsManager = GetComponentInChildren<PhysicsManager>();

            Debug.Log($"INSTANTIATED GameSystemsManager {GetEntityId()}");
        }

        private void BuildGameContext()
        {
            if (_gameContext != null) return;
            // Setup your managers
            _gameStateManager = new GameStateManager();
            // Setup your services
            _gameStateServices = new GameStateServices(_gameStateManager);
            _executeQuitService = new ExecuteQuitService();
            _quitServices = new InfrastructureServices(_gameStateManager, _executeQuitService);

            // Scene Services
            SceneEventService sceneEvents = new SceneEventService();

            var sceneLoader = GetComponentInChildren<ISceneDefinitionProvider>();
            var sceneContextProvider = new SceneContextProvider(sceneLoader);
            // Get the active scene
            string sceneName = SceneManager.GetActiveScene().name;
            SceneId sceneId = sceneContextProvider.ResolveScene(sceneName).Id;

            SceneStateManager sceneManager = new SceneStateManager(sceneEvents, sceneId);

            _sceneServices = new SceneServices(
                sceneManager,
                sceneEvents,
                _gameStateServices.GameState,
                sceneContextProvider);

            // Audio services
            var audioManager = GetComponentInChildren<AudioManager>();
            Debug.Assert(audioManager != null, "Unable to locate AudioManager in GameSystemsChildren. Is the AudioManager missing from the hierarchy?");
            _audioService = new AudioService(audioManager);

            // Event Bus
            _eventBus = new EventBus();

            // Physics Services
            var moveActor = GetComponentInChildren<IMoveActor>();
            var raycastController = GetComponentInChildren<IRaycastController>();
            _physicsServices = new PhysicsServices(moveActor, raycastController);

            // Camera service
            var camManager = GetComponentInChildren<CameraManager>();
            Debug.Assert(camManager != null, "Unable to locate CameraManager in GameSystemsChildren. Is the CameraManager missing from the hierarchy?");
            _cameraService = new CameraServices(camManager);

            // Player services
            var playerManager = GetComponentInChildren<PlayerManager>();
            Debug.Assert(playerManager != null, "Unable to locate PlayerManager in GameSystemsChildren. Is the PlayerManager missing from the hierarchy?");
            _playerServices = new PlayerServices(playerManager, playerManager, playerManager);
            Debug.Assert(_playerServices != null, $"Player services: {_playerServices} is null");

            // Scene context service
            _sceneContextService = new SceneContextService();

            // Detection services
            IScentMap scentMap = DetectionManager.Instance.ScentMap;
            _detectionServices = new DetectionServices(scentMap);

            // Npc spawn services
            _spawnNpcService = GetComponentInChildren<NpcSpawner>();

            _gameContext = new GameContext(
                            _gameStateServices,
                            _quitServices,
                            _sceneServices,
                            _audioService,
                            _eventBus,
                            _cameraService,
                            _physicsServices,
                            _playerServices,
                            _sceneContextService,
                            _detectionServices,
                            _spawnNpcService
);

            GameContextRegistry.Set(_gameContext);
        }

        private void StartScene()
        {
            Debug.Log($"Starting scene as {_sceneServices.CurrentSceneService.CurrentScene}");
            _sceneServices.SceneEvents.RaiseChangeComplete(_sceneServices.CurrentSceneService.CurrentScene);
            Debug.Log($"Actual current scene: {SceneManager.GetActiveScene().name}");

            // _physicsManager.RunSimulation = true;
            // Debug.Log($"Starting simulation");
        }

        private void OnDestroy()
        {
            Debug.Log($"DESTROYED GameSystemsManager {GetEntityId()}");
            if (Instance == this)
            {
                _bootStrapper.OnReady -= StartScene;
                Instance = null;
            }
        }

        public void Update()
        {
            if (_gameStateManager != null)
            {
                _currentGameState = _gameStateManager.CurrentState;
            }
        }

        public void RegisterBootStrapper(ISceneBootStrapper bootStrapper)
        {
            _bootStrapper = bootStrapper;
            // Subscribe to the bootstrapper's ready signal
            _bootStrapper.OnReady += StartScene;

            Debug.Log($"Registered new boot strapper");

            // Build the game context to pass to the boot strapper
            BuildGameContext();
        }
    }
}