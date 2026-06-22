using System.Collections.Generic;
using Primitives.Detection;
using Primitives.NPC;
using UnityEngine;

namespace Unity.Detection.Detectors.DataStructures
{
    [CreateAssetMenu(fileName = "DetectionMap", menuName = "Game/Detectors/Detectors Stat Map")]
    public class DetectorMapSO : ScriptableObject
    {
        [SerializeField] private List<DetectorEntry> _entries;
        private Dictionary<NpcType, DetectorStats> _statMap = new();
        private void OnEnable()
        {
            if (_entries.Count > 0)
            {
                BuildDictionary();
            }
        }
        public Dictionary<NpcType, DetectorStats> GetStatMap()
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

    [System.Serializable]
    public class DetectorEntry
    {
        public NpcType Key;
        public DetectorStatsSO Data;
    }
}