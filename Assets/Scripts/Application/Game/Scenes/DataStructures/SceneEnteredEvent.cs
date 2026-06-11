using Game.Core.Scenes;

namespace Game.Application.Scenes.DataStructures
{
    public sealed class SceneEnteredEvent
    {
        public ISceneTag SceneData { get; }

        public SceneEnteredEvent(
            ISceneTag sceneData
        )
        {
            SceneData = sceneData;
        }
    }
}