using Game.Core.Scenes;
using Primitives.Common.Scenes;

namespace Game.Application.Scenes
{
    public class SyncSceneService : ISyncSceneService
    {
        private SceneStateManager _sceneStateManager;
        public SyncSceneService(SceneStateManager sceneStateManager)
        {
            _sceneStateManager = sceneStateManager;
        }
        public void SyncScene(SceneId sceneId)
        {
            _sceneStateManager.SyncScene(sceneId);
        }
    }
}