using System.Collections.Generic;
using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Primitives.Characters;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Speaker Sound Set")]
    public class SpeakerSoundSet : ScriptableObject, ISoundSet<ISpeakerAudioRequest>
    {
        [SerializeField] private List<SpeakerSoundEntry> _entries = new();
        private Dictionary<CharacterID, AudioClip> _audioDict = new();

        private void OnEnable()
        {
            BuildDictionary();
        }

        public AudioClipSettings GetClip(ISpeakerAudioRequest request)
        {
            return new AudioClipSettings(_audioDict[request.EntityKey], Vector2.one, Vector2.one);
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