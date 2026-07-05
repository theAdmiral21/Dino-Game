using Core.PlayerController.Info;
using Game.Core.Characters.Abstractions;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using PlayerController.Core.Config;
using Primitives.Players;
using UnityEngine;

namespace PlayerController.Unity.Info
{
    public class PlayerInfoProvider : SelfRegister<IInitializable<IGameContext>>, IPlayerInfoSetter, ICharacterDataProvider, IInitializable<IGameContext>
    {
        [SerializeField] PlayerInfo _playerDebugInfo;
        public IPlayerInfo PlayerInfo => _playerInfo;
        private IPlayerInfo _playerInfo;
        public ICharacterData Data => _data;

        [SerializeField] private int _priority;
        public int Priority => _priority;

        private ICharacterData _data;
        public void Initialize(IGameContext context)
        {
            // throw new System.NotImplementedException();
        }

        public void PostInitialize(IGameContext context)
        {
            // throw new System.NotImplementedException();
        }

        public void SetPlayerInfo(IPlayerInfo playerInfo)
        {
            Debug.Assert(playerInfo != null, $"Attempted to set player info as null");
            Debug.Log($"Setting player info");
            _playerInfo = playerInfo;
            Debug.Assert(_playerInfo != null, $"Failed to set player info in PlayerInfoProvider");
        }
        public void SetCharacterData(ICharacterData data)
        {
            _data = data;
        }
    }
}