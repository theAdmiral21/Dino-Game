using Core.PlayerController.Info;
using Infrastructure.Unity.DataStructures;
using PlayerController.Core.Info;
using Primitives.Players;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class AddPlayerInfo : MonoBehaviour
    {
        public SpawnData AddInfo(IPlayerInfo playerInfo, ref SpawnData data)
        {
            // Set the player's data
            Debug.Log($"info instance (AddInfo): {playerInfo.GetHashCode()}");
            var infoProvider = data.PlayerObject.GetComponent<IPlayerInfoSetter>();
            infoProvider.SetPlayerInfo(playerInfo);
            return data;
        }
    }
}