using UnityEngine;
using System;
using Game.Application.Scenes.Abstractions;
using Primitives.Common.Scenes;
using Primitives.GameState;
using Game.Core.Scenes;
using Game.Core.State.Services;

namespace Game.Application.Scenes
{
    public class SceneChangeCoordinator : ISceneChangeCoordinator
    {
        private readonly IChangeSceneService _sceneChangeService;
        private IChangeGameStateService _changeGameStateService;

        public event Action<ISceneDefinition> EnteredNewSceneEvent;
        public event Action<ISceneDefinition> SceneEntered;

        private SceneId _requestedScene;
        private ISceneEvents _sceneEvents;

        public SceneChangeCoordinator(
             ISceneEvents sceneEvents,
             IChangeSceneService sceneChangeService,
             IChangeGameStateService changeGameStateService)
        {
            _sceneChangeService = sceneChangeService;
            _sceneEvents = sceneEvents;
            _changeGameStateService = changeGameStateService;

            _sceneEvents.SceneChangeStarted += StartSceneChange;
        }

        public void StartSceneChange(SceneId sceneId)
        {
            _changeGameStateService.ChangeGameState(GameState.Transitioning);
            _sceneEvents.RaiseSceneExit();

            // Clean out your static classes

        }

        public void FinishTransition()
        {
            // GameState initialState = _sceneStateService.GetInitialSceneState(_requestedScene);
            // Debug.Log($"Initial state for {_requestedScene} is {initialState}");
            // _changeGameStateService.ChangeGameState(initialState);

            // Notify listeners that the scene changed
            _sceneEvents.RaiseChangeComplete(_requestedScene);
            _requestedScene = SceneId.None;
            Debug.Log($"Scene change done! Frame-{Time.frameCount}");
        }
    }
}