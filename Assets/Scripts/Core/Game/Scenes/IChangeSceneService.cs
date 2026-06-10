using Primitives.Common.Scenes;

namespace Game.Core.Scenes
{
    public interface IChangeSceneService
    {
        public void ChangeScene(SceneId sceneId);
    }
}