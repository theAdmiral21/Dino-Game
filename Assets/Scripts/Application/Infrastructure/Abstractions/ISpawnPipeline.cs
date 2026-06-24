using System.Collections;
using PlayerController.Core.Events;
using PlayerController.Core.Info;
using Primitives.Players;
using UnityEngine;

namespace Infrastructure.Application.Abstractions
{
    public interface ISpawnPipeline
    {
        public IPlayerView SpawnNewPlayer(IPlayerInfo playerInfo);
        public IPlayerView SpawnNewPlayerAtLocation(IPlayerInfo playerInfo, Vector2 location);

        public IEnumerator RespawnPlayer(PlayerDiedEvent eventData);

        public IPlayerView SpawnExistingPlayer(IPlayerInfo playerInfo);
        public IPlayerView SpawnExistingPlayerAtLocation(IPlayerInfo playerInfo, Vector2 location);
    }
}