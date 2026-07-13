using Game.Core.Scenes;
using Game.Core.Scenes.Enums;
using Primitives.Common.Scenes;
using Primitives.GameState;

namespace Unity.Game.Scenes.DataStructures
{
    [System.Serializable]
    public struct SceneDefinition : ISceneDefinition
    {
        public SceneId Id { get; private set; }
        public string SceneName { get; private set; }
        public GameState StartState { get; private set; }
        public SceneType TypeOfScene { get; private set; }

        public SceneDefinition(SceneId id, string sceneName, GameState startState, SceneType typeOfScene)
        {
            Id = id;
            SceneName = sceneName;
            StartState = startState;
            TypeOfScene = typeOfScene;
        }
    }
}