using Game.Core.Scenes;
using Primitives.Common.Scenes;
using Primitives.GameState;

namespace Game.Application.Scenes
{
    public class SceneChangeRequest : ISceneChangeRequest
    {

        public SceneId RequestedScene { get; private set; }
        public SceneId CurrentScene { get; private set; }
        public GameState CurrentState { get; private set; }

        public SceneChangeRequest(SceneId requestedSceneId, SceneId currentScene, GameState gameState)
        {
            RequestedScene = requestedSceneId;
            CurrentScene = currentScene;
            CurrentState = gameState;

        }
    }
}