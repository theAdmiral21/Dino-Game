using Game.Core.Scenes;
using Game.Core.Execution;
using Primitives.Common.Scenes;
using Primitives.GameState;
using UnityEngine;
using Game.Core.State.Services;
using System.Linq;
using Infrastructure.Core.Services;
using Infrastructure.Core.Lifecycle;

namespace Game.Application.Scenes
{
    public class GameplaySceneController : BaseSceneController
    {
        private IChangeGameStateService _requestGameStateChangeService;
        private IPlayerProvider _playerProvider;
        private IDestroyPlayer _playerDestroyer;
        private ISpawnPlayer _playerSpawner;
        private ISceneDefinition _sceneDefinition;
        private ISceneContextService _sceneContextService;

        public GameplaySceneController(ISceneDefinition definition, IGameContext gameContext) : base(definition, gameContext)
        {
            _sceneDefinition = definition;
            _sceneEvents = gameContext.SceneServices.SceneEvents;

            _requestGameStateChangeService = gameContext.GameStateServices.ChangeGameState;
            _gameContext = gameContext;

            _playerProvider = _gameContext.PlayerServices.PlayerProvider;
            _playerSpawner = _gameContext.PlayerServices.SpawnPlayer;
            _playerDestroyer = _gameContext.PlayerServices.DestroyPlayer;

            _sceneContextService = _gameContext.SceneContextService;
            Debug.Log("Gameplay scene controller ready");
        }

        public override void OnSceneLoaded()
        {
            Debug.LogError($"Implement updating the scene change contexts!");
            // Consume the scene context data
            // SpawnPlayers();
            // Set the up the scene for game play

            Debug.Log($"OnSceneLoaded called");
            _requestGameStateChangeService.ChangeGameState(GameState.Gameplay);
        }

        public override void OnSceneUnloaded()
        {
            Debug.LogError($"Seriously you need to put some stuff here for when you unload the scene");

            // Destroy the players
            foreach (var player in _playerProvider.Players)
            {
                _playerDestroyer.DestroyPlayer(player);
            }

            // Update any scene change data
            // wait jk the classes do that themselves


        }

        public override void Tick()
        {
            // throw new System.NotImplementedException();
        }

        private void SpawnPlayers()
        {
            var playerList = _playerProvider.Players.ToList();

            for (int i = 0; i < playerList.Count; i++)
            {
                Vector2? location = _gameContext.SceneContextService.ConsumeOverworldPosition(playerList[i]);
                _playerSpawner.SpawnPlayer(playerList[i], location);
            }
        }
    }
}