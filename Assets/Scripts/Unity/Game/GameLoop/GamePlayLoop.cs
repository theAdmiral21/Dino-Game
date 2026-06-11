using System;
using Game.Application.Scenes;
using Game.Core.Execution;
using Game.Core.Scenes;
using Game.Core.Scenes.Enums;
using Game.Unity.Scenes.DataStructures;
using Infrastructure.Unity;
using Infrastructure.Unity.Registries;
using Primitives.Common.Scenes;
using UnityEngine;

namespace Game.Unity.GameLoop
{
    public class GamePlayLoop : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        [SerializeField] private BaseSceneContext _sceneContext;
        private ISceneRunTimeController _sceneController;

        public int Priority => 0;

        // private void Awake()
        // {
        //     SceneInitializationRegistry.Register(this);
        //     // Debug.Log($"GamePlayLoop Awake; Frame-{Time.frameCount}");
        // }

        public void Initialize(IGameContext gameContext)
        {
            // Debug.Log($"GamePlayLoop Initialize; Frame-{Time.frameCount}");
            _sceneController = CreateController(_sceneContext, gameContext);
            Debug.Log($"Running game loop with scene controller: {_sceneController}");
        }

        public void PostInitialize(IGameContext gameContext)
        {
            // Debug.Log($"GamePlayLoop Post Initialize; Frame-{Time.frameCount}");
            // Ensure we're in the correct scene
            SceneId currentScene = gameContext.SceneServices.CurrentSceneService.CurrentScene;
            if (currentScene != _sceneContext.Tag.Id)
            {
                Debug.LogError($"Current scene did not match actual scene. Updating...");
                gameContext.SceneServices.SyncSceneService.SyncScene(_sceneContext.Tag.Id);
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

        private ISceneRunTimeController CreateController(BaseSceneContext sceneContext, IGameContext gameContext)
        {
            return sceneContext.LevelType switch
            {
                SceneType.Gameplay => new GameplaySceneController(sceneContext, gameContext),
                SceneType.Cinematic => new CinematicOnlyController(sceneContext, gameContext),
                SceneType.Menu => new MenuSceneController(sceneContext, gameContext),
                _ => throw new ArgumentOutOfRangeException()
            };
        }


    }
}