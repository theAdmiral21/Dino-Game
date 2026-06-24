using System;
using System.Collections;
using Game.Core.Execution;
using Infrastructure.Application.Abstractions;
using Infrastructure.Unity.DataStructures;
using PlayerController.Core.Events;
using PlayerController.Core.Info;
using Primitives.EventBus.Abstractions;
using Primitives.Players;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class SpawnPipeLine : MonoBehaviour, ISpawnPipeline, IInitializable<IGameContext>
    {
        [SerializeField] private PlayerFactory _factory;
        [SerializeField] private SpawnResolver _spawnPoint;
        [SerializeField] private GetCharacterConfig _getCharacterConfig;
        [SerializeField] private CreateCamera _cameraCreator;
        [SerializeField] private GetSaveData _getSaveData;
        [SerializeField] private BuildPlayer _playerBuilder;
        [SerializeField] private MakePlayerInfo _makePlayerInfo;
        [SerializeField] private AddPlayerInfo _addPlayerInfo;
        // [SerializeField] private InitializeObject _initializeObject;
        [SerializeField] private SetTransitionView _setTransitionView;
        private IEventBus _eventBus;
        public int Priority => 0;

        public void Awake()
        {
            RegistryGateway.Register<IInitializable<IGameContext>>(this);
        }

        public void Initialize(IGameContext context)
        {
            _eventBus = context.EventBus;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_eventBus != null, "Unable to assign event bus");
        }

        /*
        Something to be aware of: Respawning a player is different from spawning a new player. When respawning a player they are temporarily disabled and then sent back to the last check point. So they do NOT need to be rebuilt and I don't think I need the character data. We're essentially teleporting the player to the respawn. What happens if I do delete the player and respawn them?
        */
        public IEnumerator RespawnPlayer(PlayerDiedEvent eventData)
        {
            Debug.Log($"eventData: {eventData.OverrideControls} in routine");
            // Disable the player
            eventData.OverrideControls.DisablePlayer();
            // Move the player
            Guid playerId = eventData.PlayerView.PlayerInfo.PlayerInfo.PlayerId;
            eventData.OverrideControls.OverrideMove(_spawnPoint.GetRespawnPoint(playerId));
            // Reenable the player
            eventData.OverrideControls.EnablePlayer();
            // Raise the event
            _eventBus.Publish(new PlayerSpawnedEvent { PlayerView = eventData.PlayerView });
            yield return null;
        }

        public IPlayerView SpawnExistingPlayer(IPlayerInfo playerInfo)
        {
            // Make the spawn data
            SpawnData data = new()
            {
                PlayerId = playerInfo.PlayerId,
                Id = playerInfo.CharacterId,
            };
            IPlayerView view = RunPipeline(data, playerInfo);

            // Resolve the spawn point
            data = _spawnPoint.GetSpawnPoint(ref data);

            return view;
        }

        public IPlayerView SpawnExistingPlayerAtLocation(IPlayerInfo playerInfo, Vector2 location)
        {
            // Make the spawn data
            SpawnData data = new()
            {
                PlayerId = playerInfo.PlayerId,
                Id = playerInfo.CharacterId,
                SpawnPoint = location,
            };
            IPlayerView view = RunPipeline(data, playerInfo);

            return view;
        }

        public IPlayerView SpawnNewPlayer(IPlayerInfo playerInfo)
        {
            // Make the player's info
            // var playerInfo = _makePlayerInfo.MakeInfo(playerId, characterID);
            // Make the spawn data
            SpawnData data = new()
            {
                PlayerId = playerInfo.PlayerId,
                Id = playerInfo.CharacterId,
            };
            // Resolve the spawn point
            data = _spawnPoint.GetSpawnPoint(ref data);

            IPlayerView view = RunPipeline(data, playerInfo);

            return view;
        }

        public IPlayerView SpawnNewPlayerAtLocation(IPlayerInfo playerInfo, Vector2 location)
        {
            // Make the player's info
            // var playerInfo = _makePlayerInfo.MakeInfo(playerId, characterID);
            // Make the spawn data
            SpawnData data = new()
            {
                PlayerId = playerInfo.PlayerId,
                Id = playerInfo.CharacterId,
                SpawnPoint = location,
            };

            IPlayerView view = RunPipeline(data, playerInfo);

            return view;
        }

        private IPlayerView RunPipeline(SpawnData data, IPlayerInfo playerInfo)
        {

            // Add the player object
            Debug.Log($"Spawning new player - frame {Time.frameCount}");
            data = _factory.InstantiateObject(ref data);
            // Add the camera object
            data = _cameraCreator.InstantiateObject(ref data);
            // Initialize the objects
            // data = _initializeObject.InitializePlayer(ref data);
            // Set the transition view
            data = _setTransitionView.SetView(ref data);
            // Set the player info
            data = _addPlayerInfo.AddInfo(playerInfo, ref data);
            // Get the save data
            data = _getSaveData.Fetch(ref data);
            // Get the character config
            data = _getCharacterConfig.GetCharacterData(ref data);
            // Build the player
            _playerBuilder.Build(data);

            // Add the new player to the game manager's list
            PlayerManager.Instance.TrackPlayer(data.PlayerObject);

            // Return the view
            IPlayerView view = data.PlayerObject.GetComponentInChildren<IPlayerView>();

            return view;
        }
    }
}