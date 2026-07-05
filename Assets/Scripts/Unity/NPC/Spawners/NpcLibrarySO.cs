using System.Collections.Generic;
using Primitives.Audio.EntityKeys;
using Primitives.Items;
using UnityEngine;

namespace Unity.Equipment.DataStructures
{
    [System.Serializable]
    public class NpcEntry
    {
        public EnemyEntityKey Key;
        public GameObject Data;
    }

    [CreateAssetMenu(fileName = "NpcLibrary", menuName = "Game/NPC/NPC Library")]
    public class NpcLibrarySO : ScriptableObject
    {
        [SerializeField] private List<NpcEntry> _entries;
        private Dictionary<EnemyEntityKey, GameObject> _npcDict = new();
        private void OnEnable()
        {
            BuildDictionary();
        }
        public Dictionary<EnemyEntityKey, GameObject> GetDict()
        {
            if (_npcDict.Keys.Count == 0)
            {
                BuildDictionary();
            }
            return _npcDict;
        }

        private void BuildDictionary()
        {
            _npcDict.Clear();

            foreach (var entry in _entries)
            {
                if (!_npcDict.ContainsKey(entry.Key))
                {
                    _npcDict[entry.Key] = entry.Data;
                }
                else
                {
                    Debug.LogError($"{entry.Key} has already been added to the dictionary. Do you have multiples of the same entry?");
                }
            }
        }
    }
}
