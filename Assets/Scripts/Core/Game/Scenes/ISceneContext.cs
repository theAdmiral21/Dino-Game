using Game.Core.Scenes.Enums;
using Primitives.GameState;

namespace Game.Core.Scenes
{
    public interface ISceneDefinition
    {
        public ISceneTag Tag { get; }
        public GameState StartState { get; }
        public SceneType LevelType { get; }
    }
}