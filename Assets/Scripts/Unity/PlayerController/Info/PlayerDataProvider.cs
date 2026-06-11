using Game.Core.Characters.Abstractions;
using Movement.Core.Abstractions;
using Movement.Core.Stats;
using Movement.Unity.Abstractions;
using PlayerController.Core.Config;
using PlayerController.Core.Info;
using Primitives.Players;
using Unity.Common.Unity;
using UnityEngine;

namespace PlayerController.Unity.Info
{
    public class PlayerDataProvider : MonoBehaviour, IPlayerInfoProvider, IStatProvider, ICharacterDataProvider
    {
        [SerializeField] PlayerInfo _playerDebugInfo;
        public IPlayerInfo PlayerInfo => _playerInfo;
        private IPlayerInfo _playerInfo;

        [SerializeField] private SerializedInterface<IStatSheet> _statSheetMono;
        public IStatSheet StatSheet => _statSheetMono.Interface;
        private IStatCollection _stats => StatSheet.StatCollection;

        // private PlayerStats _stats;
        // public PlayerStats Stats
        // {
        //     get
        //     {
        //         if (!_statsSet)
        //         {
        //             SetStats(_statSO);
        //         }
        //         return _stats;
        //     }
        // }

        public ICharacterData Data => _data;


        private ICharacterData _data;

        public void SetPlayerInfo(IPlayerInfo playerInfo)
        {
            Debug.Log($"Setting player info");
            _playerInfo = playerInfo;

        }

        // public void SetStats(ScriptableObject statSO)
        // {
        //     _statInterface = _statSO as IMovementStats;
        //     if (_statInterface == null)
        //     {
        //         Debug.LogError($"Unable to convert {_statSO.name} to IMovementStats.");
        //         return;
        //     }
        //     // _stats = new PlayerStats(_statInterface);
        //     _statsSet = true;
        // }

        public void SetCharacterData(ICharacterData data)
        {
            _data = data;
        }
    }
}