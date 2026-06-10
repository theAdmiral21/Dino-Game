using UnityEngine;
using Primitives.Players;

namespace Infrastructure.Core.Lifecycle
{
    public interface ISpawnPlayer
    {
        public void SpawnPlayer(IPlayerInfo player, Vector2? location = null);
    }
}