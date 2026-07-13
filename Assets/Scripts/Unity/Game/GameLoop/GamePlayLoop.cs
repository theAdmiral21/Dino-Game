using System;
using Game.Application.Scenes;
using Game.Core.Execution;
using Game.Core.Scenes;
using Game.Core.Scenes.Enums;
using Game.Unity.Scenes.DataStructures;
using Infrastructure.Unity.Registries;
using Primitives.Common.Scenes;
using Unity.Game.Scenes.DataStructures;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Unity.GameLoop
{
    public class GamePlayLoop : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        private ISceneDefinition _sceneDef;
        private ISceneRunTimeController _sceneController;

        [SerializeField] private int _priority = 0;
        public int Priority => _priority;

        // private void Awake()
        // {
        //     SceneInitializationRegistry.Register(this);
        //     // Debug.Log($"GamePlayLoop Awake; Frame-{Time.frameCount}");
        // }

        public void Initialize(IGameContext gameContext)
        {
            // Get the current scene's name
            string sceneName = SceneManager.GetActiveScene().name;
            // Get the scene definition
            _sceneDef = gameContext.SceneServices.SceneDefinitionProvider.ResolveScene(sceneName);
            _sceneController = CreateController(_sceneDef, gameContext);
            Debug.Log($"Running game loop with scene controller: {_sceneController}");
        }

        public void PostInitialize(IGameContext gameContext)
        {
            // Debug.Log($"GamePlayLoop Post Initialize; Frame-{Time.frameCount}");
            // Ensure we're in the correct scene
            Debug.Log($"[SceneManager] Active scene: {SceneManager.GetActiveScene().name}");
            SceneId currentScene = gameContext.SceneServices.CurrentSceneService.CurrentScene;
            if (currentScene != _sceneDef.Id)
            {
                Debug.LogError($"Current scene ({currentScene}) did not match actual scene {_sceneDef.Id}. Updating...");
                gameContext.SceneServices.SyncSceneService.SyncScene(_sceneDef.Id);
            }
        }
        private void Update()
        {
            _sceneController?.Tick();
        }

        private void OnDestroy()
        {
            // RegistryGateway.ClearRegistries();
            _sceneController?.OnSceneUnloaded();
            base.OnDestroy();
        }

        private ISceneRunTimeController CreateController(ISceneDefinition sceneContext, IGameContext gameContext)
        {
            return sceneContext.TypeOfScene switch
            {
                SceneType.Gameplay => new GameplaySceneController(sceneContext, gameContext),
                SceneType.Cinematic => new CinematicOnlyController(sceneContext, gameContext),
                SceneType.Menu => new MenuSceneController(sceneContext, gameContext),
                _ => throw new ArgumentOutOfRangeException()
            };
        }


    }
}