using Primitives.Common.Scenes;
using Primitives.GameState;

namespace Game.Core.Scenes
{
    public interface ISceneChangeRequest
    {
        public SceneId RequestedScene { get; }
        public SceneId CurrentScene { get; }
        public GameState CurrentState { get; }
    }
}