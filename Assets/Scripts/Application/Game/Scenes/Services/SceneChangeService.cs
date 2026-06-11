using System;
using Game.Core.Scenes;
using Game.Core.State.Services;
using Primitives.Common.Scenes;

namespace Game.Application.Scenes.Services
{
    /// <summary>
    /// Service for requesting a scene change from the scene status manager.
    /// </summary>
    public class SceneChangeService : IChangeSceneService
    {
        public event Action<SceneId> OnSceneChangeRequested;
        private readonly ISceneChangeController _sceneStatusManager;
        private ICurrentSceneProvider _currentScene;
        private IGameStateProvider _gameState;
        public SceneChangeService(ISceneChangeController sceneStatusManager, ICurrentSceneProvider currentSceneProvider, IGameStateProvider currentGameState)
        {
            _sceneStatusManager = sceneStatusManager;
            _currentScene = currentSceneProvider;
            _gameState = currentGameState;
        }

        public void ChangeScene(SceneId id)
        {
            // Debug.Log($"Request scene change to {id}");
            _sceneStatusManager.RequestSceneChange(new SceneChangeRequest(id, _currentScene.CurrentScene, _gameState.CurrentState));
        }
    }
}