using System.Collections;
using Primitives.Effects;
using Game.Core.Execution;
using Infrastructure.Application.Abstractions;
using PlayerController.Core.Events;
using Primitives.EventBus.Abstractions;
using Unity.Common.Unity;
using UnityEngine;
using System;
using PlayerController.Core.Info;
using System.Collections.Generic;
using Game.Core.Scenes.Enums;
using Game.Core.Scenes;
using Primitives.Common.Scenes;
using Primitives.Players;

namespace Infrastructure.Unity.Status
{
    public class PlayerLifeCycleHandler : MonoBehaviour, IInitializable<IGameContext>
    {
        [SerializeField] private SerializedInterface<ISpawnPipeline> _spawnPipeline;
        [SerializeField] private ScreenTransitions _transition;
        private IEventBus _eventBus;
        private ISceneDefinitionProvider _sceneContextProvider;
        private ISceneDefinition _sceneContext => _sceneContextProvider.ResolveScene(_currentSceneProvider.CurrentScene);
        private ICurrentSceneProvider _currentSceneProvider;
        private SceneId _currentScene => _currentSceneProvider.CurrentScene;

        private HashSet<Guid> _respawnTargets = new();
        public int Priority => 50;

        private void Awake()
        {
            RegistryGateway.Register<IInitializable<IGameContext>>(this);
        }

        private void OnDestroy()
        {
            Debug.Log($"Destroying PlayerLifeCycleHandler: {GetHashCode()}");
            RegistryGateway.Deregister<IInitializable<IGameContext>>(this);
        }

        public void Initialize(IGameContext context)
        {
            _eventBus = context.EventBus;
            _sceneContextProvider = context.SceneServices.SceneDefinitionProvider;
            _currentSceneProvider = context.SceneServices.CurrentSceneService;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Log($"The current scene is: {_currentScene} of type: {_sceneContext}");


            _eventBus.Subscribe<PlayerDiedEvent>(RunRespawn);
        }

        public void SpawnNewPlayer(IPlayerInfo playerInfo)
        {
            StartCoroutine(SpawnNewRoutine(playerInfo));
        }

        // private IEnumerator SpawnRoutine(Guid playerId, CharacterID characterID)
        // {
        //     Debug.Log($"Spawning player");
        //     IPlayerView playerView = _spawnPipeline.Interface.SpawnNewPlayer(playerId, characterID);

        //     // Fade Out
        //     if (_sceneContext.LevelType == SceneType.Dolphin)
        //     {
        //         yield return playerView.TransitionView.PlayOutTransition(ScreenTransitions.Dolphin);
        //     }
        //     else
        //     {
        //         yield return playerView.TransitionView.PlayOutTransition(_transition);
        //     }
        // }

        public void SpawnNewPlayerAtLocation(IPlayerInfo playerInfo, Vector2 location)
        {
            StartCoroutine(SpawnNewRoutine(playerInfo, location));
        }

        private IEnumerator SpawnNewRoutine(IPlayerInfo playerInfo, Vector2? location = null)
        {
            Debug.Log($"Spawning player");
            IPlayerView playerView;
            if (location == null)
            {
                playerView = _spawnPipeline.Interface.SpawnNewPlayer(playerInfo);
            }
            else
            {
                playerView = _spawnPipeline.Interface.SpawnNewPlayerAtLocation(playerInfo, (Vector2)location);
            }
            yield return StartCoroutine(PlayTransitionRoutine(playerView));

        }
        public void SpawnExistingPlayer(IPlayerInfo playerInfo)
        {
            StartCoroutine(SpawnExistingRoutine(playerInfo));
        }
        public void SpawnExistingPlayerAtLocation(IPlayerInfo playerInfo, Vector2 location)
        {
            StartCoroutine(SpawnExistingRoutine(playerInfo, location));
        }
        private IEnumerator SpawnExistingRoutine(IPlayerInfo playerInfo, Vector2? location = null)
        {
            Debug.Log($"Spawning player");
            IPlayerView playerView;
            if (location == null)
            {
                playerView = _spawnPipeline.Interface.SpawnExistingPlayer(playerInfo);
            }
            else
            {
                playerView = _spawnPipeline.Interface.SpawnExistingPlayerAtLocation(playerInfo, (Vector2)location);
            }
            yield return StartCoroutine(PlayTransitionRoutine(playerView));
        }

        private IEnumerator PlayTransitionRoutine(IPlayerView playerView)
        {
            // Fade Out
            if (_sceneContext.LevelType == SceneType.Dolphin)
            {
                yield return playerView.TransitionView.PlayOutTransition(ScreenTransitions.Dolphin);
            }
            else
            {
                yield return playerView.TransitionView.PlayOutTransition(_transition);
            }
        }

        public void RunRespawn(PlayerDiedEvent eventData)
        {

            StartCoroutine(RespawnRoutine(eventData));

        }

        private IEnumerator RespawnRoutine(PlayerDiedEvent eventData)
        {
            // Raise a flag so this isn't called more than once for the same player
            Guid player = eventData.PlayerView.PlayerInfo.PlayerInfo.PlayerId;
            if (_respawnTargets.Add(player))
            {
                eventData.OverrideControls.DisablePlayer();
                // Fade In
                yield return eventData.PlayerView.TransitionView.PlayInTransition(_transition);
                // Respawn
                yield return _spawnPipeline.Interface.RespawnPlayer(eventData);
                // Fade Out
                yield return eventData.PlayerView.TransitionView.PlayOutTransition(_transition);

                _respawnTargets.Remove(player);
            }
        }

        private IEnumerator FailDolphinRoutine(PlayerDiedEvent eventData)
        {
            // Raise a flag so this isn't called more than once for the same player
            Guid player = eventData.PlayerView.PlayerInfo.PlayerInfo.PlayerId;
            if (_respawnTargets.Add(player))
            {
                eventData.OverrideControls.DisablePlayer();
                // Fade In
                yield return eventData.PlayerView.TransitionView.PlayInTransition(ScreenTransitions.Dolphin);
            }
        }
    }
}