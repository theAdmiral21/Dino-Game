using Game.Core.Scenes;
using Game.Core.Scenes.Enums;
using Primitives.GameState;
using UnityEngine;

namespace Game.Unity.Scenes.DataStructures
{
    public abstract class BaseSceneContext : ScriptableObject, ISceneDefinition
    {
        // Identifying tag for this scene context
        public ISceneTag Tag => _sceneTag;
        [SerializeField] private SceneTag _sceneTag;
        // The starting state of this scene
        public GameState StartState => _sceneState;
        [SerializeField] private GameState _sceneState;
        // The type of level this is
        public SceneType LevelType => _levelType;
        [SerializeField] private SceneType _levelType;
    }
}