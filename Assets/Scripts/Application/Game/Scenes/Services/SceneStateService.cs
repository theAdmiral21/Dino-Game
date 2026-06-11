using System.Collections.Generic;
using Game.Application.Scenes.Abstractions;
using Game.Core.Scenes;
using Game.Core.Scenes.Enums;
using Game.Scenes.Application;
using Primitives.Common.Scenes;
using Primitives.GameState;

namespace Game.Application.Scenes.Services
{
    public class SceneStateService : ISceneStateProvider
    {
        private Dictionary<SceneType, GameState> _stateMap = new()
        {
        {SceneType.Menu,GameState.InMenu},
        {SceneType.Cinematic,GameState.InCutscene},
        {SceneType.Gameplay,GameState.Gameplay},
        };

        private ISceneLoader _sceneLoader;
        public SceneStateService(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;

        }
        public GameState GetInitialSceneState(SceneId sceneId)
        {
            ISceneDefinition sceneData = _sceneLoader.ResolveScene(sceneId);
            return sceneData.StartState;
            // if (_stateMap.TryGetValue(sceneData.StartState, out var state))
            //     return state;

        }
    }
}