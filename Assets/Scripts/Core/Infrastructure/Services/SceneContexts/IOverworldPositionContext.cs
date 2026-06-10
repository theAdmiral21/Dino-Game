using Primitives.Players;
using UnityEngine;

namespace Infrastructure.Core.Services
{
    public interface IOverworldPositionContext
    {
        public void SetOverworldPosition(IPlayerInfo player, Vector2 pos);
        public Vector2? ConsumeOverworldPosition(IPlayerInfo player);
    }
}