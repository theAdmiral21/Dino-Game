using UnityEngine;
using Game.Systems.Scenes.Application;
using Primitives.Common.Scenes;
using Game.Core.Scenes;

namespace Game.Application.Scenes
{
    public class SceneStateManager : ISceneChangeController, ISyncSceneService
    {

        public SceneId CurrentScene => _currentScene;
        // Default to boot scene
        private SceneId _currentScene;

        private ISceneEvents _sceneEventService;

        private ISceneTransitionPolicy _sceneChangePolicy = new SceneTransitionPolicy();

        public SceneStateManager(ISceneEvents sceneEvents, SceneId currentScene)
        {
            _sceneEventService = sceneEvents;
            _currentScene = currentScene;
        }

        public bool RequestSceneChange(ISceneChangeRequest request)
        {
            if (_sceneChangePolicy.EvaluateRequest(request))
            {
                Debug.Log($"Changing from {_currentScene} to {request.RequestedScene}");
                AlertSceneChangeStarted(request.RequestedScene);
                return true;
            }
            Debug.Log($"Scene change request denied");
            return false;
        }

        private void AlertSceneChangeStarted(SceneId newScene)
        {
            _currentScene = newScene;
            _sceneEventService.RaiseSceneChangeStarted(_currentScene);
        }

        public void SyncScene(SceneId sceneId)
        {
            _currentScene = sceneId;
        }
    }
}