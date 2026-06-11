using Game.Core.Scenes;
using Primitives.Common.Scenes;

namespace Game.Application.Scenes.Services
{
    public class CurrentSceneProviderService : ICurrentSceneProvider
    {
        public SceneId CurrentScene => _sceneStatusManager.CurrentScene;
        private SceneStateManager _sceneStatusManager;
        public CurrentSceneProviderService(SceneStateManager sceneStatusManager)
        {
            _sceneStatusManager = sceneStatusManager;
        }
    }
}