using UnityEngine;
using Game.Core.Cinematics.Abstractions;
using Primitives.Common.Scenes;
using System.Collections.Generic;
using Game.Application.Scenes.Abstractions;
using Game.Core.Cinematics.Enums;
using Game.Core.Scenes;
using Game.Core.Execution;

namespace Game.Application.Scenes
{
    public class CinematicOnlyController : BaseSceneController
    {
        private ICinematicOnlySceneContext _sceneContext;
        private List<CinematicId> _cinematics = new();
        private IChangeSceneService _sceneChangeService;
        private SceneId _nextScene;
        private int _index = 0;
        public CinematicOnlyController(ISceneDefinition sceneContext, IGameContext gameContext) : base(sceneContext, gameContext)
        {
            _sceneContext = sceneContext as ICinematicOnlySceneContext;
            if (_sceneContext == null)
            {
                Debug.LogError($"Unable to convert {sceneContext} to ICinematicOnlySceneContext");
                return;
            }
            _sceneChangeService = gameContext.SceneServices.SceneChangeService;
            _nextScene = _sceneContext.NextScene.Id;
            _cinematics = _sceneContext.Cinematics;

        }
        public override void OnSceneLoaded()
        {
            Debug.Log($"OnSceneLoaded called");
            Debug.Log($"_cinematics: {_cinematics}");
            Debug.Log($"Index: {_index}");
            // Debug.Log($"Cinematic registry is not empty: {CinematicRegistry.Cinematics[_cinematics[_index]] != null}");
            if (CinematicRegistry.TryGet(_cinematics[_index], out ICinematicService cinematic))
            {
                Debug.Log($"Playing cinematic: {_cinematics[_index]}");
                cinematic.PlayCinematic(OnCinematicComplete);
            }
        }

        private void OnCinematicComplete()
        {
            _index++;
            Debug.Log($"Cut scene complete! index: {_index} count: {_cinematics.Count}");
            if (_cinematics.Count <= _index)
            {
                // Move to the next scene
                Debug.Log($"Go to scene {_nextScene}");
                _sceneChangeService.ChangeScene(_nextScene);
            }
        }
        public override void OnSceneUnloaded()
        {

        }

        public override void Tick()
        {
        }
    }
}