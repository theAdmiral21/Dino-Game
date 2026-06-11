using System;
using PlayerController.Core.Config;
using PlayerController.Core.Info;
using Primitives.Characters;
using Primitives.Players;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class MakePlayerInfo : MonoBehaviour
    {
        public IPlayerInfo MakeInfo(Guid playerId, CharacterID characterID)
        {
            var info = new PlayerInfo(playerId, characterID);
            Debug.Log($"Implement fetching save data");
            return info;
        }
    }
}