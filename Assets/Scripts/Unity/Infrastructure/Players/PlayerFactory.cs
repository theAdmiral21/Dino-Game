using Infrastructure.Unity.DataStructures;
using UnityEngine;

namespace Infrastructure.Unity.Players
{
    public class PlayerFactory : BaseInitFactory
    {
        [SerializeField] private GameObject _playerPreFab;

        public override SpawnData InstantiateObject(ref SpawnData data)
        {
            var playerObject = Instantiate(_playerPreFab, data.SpawnPoint, Quaternion.identity);
            playerObject = InitializeNewObject(playerObject);
            playerObject.SetActive(false);

            data.PlayerObject = playerObject;
            return data;
        }
    }
}