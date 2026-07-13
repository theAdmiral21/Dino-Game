using Game.Core.Scenes.Enums;
using Primitives.Common.Scenes;
using Primitives.GameState;

namespace Game.Core.Scenes
{
    public interface ISceneDefinition
    {
        // public ISceneTag Tag { get; }
        public SceneId Id { get; }
        public string SceneName { get; }
        public GameState StartState { get; }
        public SceneType TypeOfScene { get; }
    }
}