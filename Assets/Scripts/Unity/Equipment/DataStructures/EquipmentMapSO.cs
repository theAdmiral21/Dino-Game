using System.Collections.Generic;
using Primitives.Items;
using UnityEngine;

namespace Unity.Equipment.DataStructures
{
    [CreateAssetMenu(fileName = "StatMap", menuName = "Game/Items/Equipment Stats Map")]
    public class EquipmentMapSO : ScriptableObject
    {
        [SerializeField] private List<StatEntry> _entries;
        private Dictionary<ItemType, EquipmentStats> _statMap = new();
        private void OnEnable()
        {
            BuildDictionary();
        }
        public Dictionary<ItemType, EquipmentStats> GetStatMap()
        {
            if (_statMap.Keys.Count == 0)
            {
                BuildDictionary();
            }
            return _statMap;
        }

        private void BuildDictionary()
        {
            _statMap.Clear();

            foreach (var entry in _entries)
            {
                if (!_statMap.ContainsKey(entry.Key))
                {
                    _statMap[entry.Key] = entry.Data.BuildRunTime();
                }
                else
                {
                    Debug.LogError($"{entry.Key} has already been added to the dictionary. Do you have multiples of the same entry?");
                }
            }
        }
    }
}
