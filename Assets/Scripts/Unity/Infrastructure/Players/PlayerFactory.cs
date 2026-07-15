using Core.PlayerController.Info;
using Infrastructure.Unity.DataStructures;
using Primitives.Players;
using Unity.Game.GameLoop;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPreFab;

        public SpawnData InstantiateObject(IPlayerInfo playerInfo, ref SpawnData data)
        {
            var playerObject = Instantiate(_playerPreFab, data.SpawnPoint.Value, Quaternion.identity);
            // Before you can initialize things, you need to make sure the player's info is set
            var infoProvider = playerObject.GetComponent<IPlayerInfoSetter>();
            infoProvider.SetPlayerInfo(playerInfo);

            // Initialize everything
            InitFactory.InitializeObject(GameContextRegistry.GameContext, playerObject);

            // Set the saved/default data
            InitFactory.InitSavedData(playerObject, data.SaveData);
            playerObject.SetActive(false);

            data.PlayerObject = playerObject;
            return data;
        }
    }
}