using System;
using System.Collections.Generic;
using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Level Object Sound Set")]
    public class LevelObjectSoundSet : ScriptableObject, ISoundSet
    {
        public List<LevelObjectSoundEntry> Entries => _entries;
        [SerializeField] private List<LevelObjectSoundEntry> _entries = new();
        private Dictionary<ValueTuple<EntityKey, ActionSoundKey>, LevelObjectSoundEntry> _audioDict = new();
        public Vector2 VolumeRange = new Vector2(0.95f, 1.05f);
        public Vector2 PitchRange = new Vector2(0.95f, 1.05f);
        private void OnEnable()
        {
            BuildDictionary();
        }

        public AudioClipSettings GetClip(IAudioRequest request)
        {
            if (_audioDict.TryGetValue((request.Entity, request.ActionKey), out LevelObjectSoundEntry entry))
            {
                Vector2 pitchRange = PitchRange;
                Vector2 volumeRange = VolumeRange;

                if (!entry.ModulatePitch)
                {
                    pitchRange = Vector2.one;
                }
                if (!entry.ModulateVolume)
                {
                    volumeRange = Vector2.one;
                }
                return new AudioClipSettings(entry.Clip, volumeRange, pitchRange);
            }
            Debug.LogError($"{name} could not map {(request.Entity, request.ActionKey)} to a sound.");
            return null;
        }

        private void BuildDictionary()
        {
            _audioDict = new();
            foreach (var entry in _entries)
            {
                if (!_audioDict.ContainsKey((entry.Key, entry.ActionKey)))
                {
                    _audioDict[(entry.Key, entry.ActionKey)] = entry;
                }
                else
                {
                    Debug.LogError($"ItemSoundKey {(entry.Key, entry.ActionKey)} is already paired with audio clip {entry.Clip} in the item sound key dictionary.");
                }
            }
        }
    }
}