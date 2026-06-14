using System.Collections.Generic;
using Primitives.Items;
using UnityEngine;

namespace Unity.Inventory.DataStructures
{
    [CreateAssetMenu(fileName = "ItemLimits", menuName = "Game/Items/Carry Limits")]
    public class InventoryLimitSO : ScriptableObject
    {
        [SerializeField] private int _taserLimit;
        [SerializeField] private int _shotgunLimit;
        [SerializeField] private int _rocketLauncherLimit;
        [SerializeField] private int _nerveGasLimit;
        [SerializeField] private int _shellLimit;
        [SerializeField] private int _rocketLimit;
        [SerializeField] private int _canisterLimit;
        [SerializeField] private int _rockLimit;
        [SerializeField] private int _flashlightLimit;
        [SerializeField] private int _medkitLimit;
        [SerializeField] private int _flaresLimit;
        [SerializeField] private int _smokeGrenadeLimit;
        private Dictionary<ItemType, int> _dict = new();
        private void OnEnable()
        {
            BuildDictionary();
        }

        public Dictionary<ItemType, int> GetLimitMap()
        {
            if (_dict.Keys.Count == 0)
            {
                BuildDictionary();
            }
            return _dict;
        }

        private void BuildDictionary()
        {
            _dict = new Dictionary<ItemType, int>
            {
                [ItemType.Taser] = _taserLimit,
                [ItemType.Shotgun] = _shotgunLimit,
                [ItemType.RocketLauncher] = _rocketLauncherLimit,
                [ItemType.NerveGas] = _nerveGasLimit,
                [ItemType.Shell] = _shellLimit,
                [ItemType.Rocket] = _rocketLimit,
                [ItemType.Canister] = _canisterLimit,
                [ItemType.Rock] = _rockLimit,
                [ItemType.Flashlight] = _flashlightLimit,
                [ItemType.Medkit] = _medkitLimit,
                [ItemType.Flares] = _flaresLimit,
                [ItemType.SmokeGrenade] = _smokeGrenadeLimit,
            };

            foreach (var key in _dict.Keys)
            {
                if (_dict[key] <= 0)
                {
                    Debug.LogError($"{key} can not be less than or equal to zero!");
                }
            }
        }
    }
}