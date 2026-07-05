using UnityEngine;
using Game.Application.Scenes;
using Primitives.Common.Scenes;
using Infrastructure.Unity.Registries;
using Game.Core.Execution;
using Game.Core.Scenes;
using Primitives.GameState;
using Game.Core.State.Services;

namespace Game.Unity.Scenes
{
    public sealed class UnitySceneFlowManager : SelfRegister<IInitializable<IGameContext>>, IUnitySceneFlowManager, IInitializable<IGameContext>
    {

        public SceneTransitionRunner Runner => _runner;
        [SerializeField] private SceneTransitionRunner _runner;
        public bool IsTransitioning => _isTransitioning;
        private bool _isTransitioning;

        private IGameStateEvents _gameStateEvents;
        private SceneChangeCoordinator _sceneFlowCoordinator;
        public static UnitySceneFlowManager Instance { get; private set; }

        [SerializeField] private int _priority = 0;
        public int Priority => _priority;
        private ISceneEvents _sceneEvents;
        private SceneId _targetScene;
        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }

        public void Initialize(IGameContext context)
        {
            _gameStateEvents = context.GameStateServices.GameStateEvents;
            // Subscribe to game state changes
            _gameStateEvents.OnGameStateChanged += HandleStateChange;

            _sceneEvents = context.SceneServices.SceneEvents;
            _sceneEvents.SceneChangeStarted += HandleSceneChangeStart;
        }

        private void HandleSceneChangeStart(SceneId id)
        {
            _targetScene = id;
        }

        public void PostInitialize(IGameContext context)
        {
            if (_sceneFlowCoordinator == null)
            {
                _sceneFlowCoordinator = new SceneChangeCoordinator(
                    context.SceneServices.SceneEvents,
                    context.SceneServices.SceneChangeService,
                    context.GameStateServices.ChangeGameState
                    );
            }
        }
        // NOTE FIgure out what you're going to do with this. It's important but how important? It is supposed to be the entry into the scene change machinery but boy is it not...
        public void OnSceneChangeRequested(SceneId target)
        {
            Debug.Log($"Someone requested a scene change?");
            // Update where we intend to go
            // _targetScene = target;
            // Ask the manager if you can change not the coordinator.
            // _sceneFlowCoordinator.StartSceneChange(target);

            // if (!approved)
            // {
            //     Debug.LogError($"Scene change to {target} was denied");
            //     return;
            // }

        }

        private void HandleStateChange(GameState gameState)
        {
            // Debug.Log($"flow manager got game state: {gameState}");
            if (gameState != GameState.Transitioning) return;

            // Start changing scenes
            // Debug.Log($"Transitioning to {_targetScene}");
            _runner.RunSceneTransition(_targetScene, _sceneFlowCoordinator.FinishTransition);
        }


    }
}