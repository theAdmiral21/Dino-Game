using Primitives.Common.Scenes;
using Primitives.GameState;

namespace Game.Application.Scenes.Abstractions
{
    public interface ISceneStateProvider
    {
        public GameState GetInitialSceneState(SceneId sceneId);
    }
}