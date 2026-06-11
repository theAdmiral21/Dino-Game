using System.Collections.Generic;
using Primitives.Characters;
using UnityEngine;

namespace Infrastructure.Unity.DataStructures
{
    [CreateAssetMenu(fileName = "New Portrait data", menuName = "Configs/Character Portrait")]
    public class PortraitData : ScriptableObject
    {
        [SerializeField] private List<PortraitDataEntry> _entries = new();
        private Dictionary<PortraitEmotion, PortraitDataEntry> _spriteDict = new();
        private void OnEnable()
        {
            BuildDictionary();
        }
        private void BuildDictionary()
        {
            _spriteDict = new();
            foreach (var entry in _entries)
            {
                if (!_spriteDict.ContainsKey(entry.Key))
                {
                    _spriteDict[entry.Key] = entry;
                }
                else
                {
                    Debug.LogError($"PortraitEmotion {entry.Key} is already paired with data entry {entry}.");
                }
            }
        }
        public Sprite[] GetSprites(PortraitEmotion key)
        {
            if (_spriteDict.TryGetValue(key, out PortraitDataEntry entry))
            {
                return entry.Clip;

            }
            throw new KeyNotFoundException($"Unable to find value for key: {key}");
        }
    }
}