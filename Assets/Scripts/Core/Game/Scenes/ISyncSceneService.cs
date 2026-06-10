using Primitives.Common.Scenes;

namespace Game.Core.Scenes
{
    public interface ISyncSceneService
    {
        public void SyncScene(SceneId sceneId);
    }
}