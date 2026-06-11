using System.Collections.Generic;
using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Item Sound Set")]
    public class ItemSoundSet : ScriptableObject, ISoundSet<IItemAudioRequest>
    {
        [SerializeField] private List<ItemSoundEntry> _entries = new();
        private Dictionary<ItemSoundKey, AudioClip> _audioDict = new();

        private void OnEnable()
        {
            BuildDictionary();
        }

        public void AddNewEntry(ItemSoundEntry entry)
        {
            _entries.Add(entry);
        }

        public AudioClipSettings GetClip(IItemAudioRequest request)
        {
            return new AudioClipSettings(_audioDict[request.ActionKey], Vector2.one, Vector2.one);
        }

        private void BuildDictionary()
        {
            _audioDict = new();
            foreach (var entry in _entries)
            {
                if (!_audioDict.ContainsKey(entry.Key))
                {
                    _audioDict[entry.Key] = entry.Clip;
                }
                else
                {
                    Debug.LogError($"ItemSoundKey {entry.Key} is already paired with audio clip {entry.Clip} in the item sound key dictionary.");
                }
            }
        }
    }
}