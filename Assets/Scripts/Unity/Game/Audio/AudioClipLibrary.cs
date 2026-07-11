using System.Collections.Generic;
using System.Linq;
using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Game.Unity.Audio.DataStructures;
using Primitives.Audio.EntityKeys;
using UnityEngine;

namespace Game.Unity.Audio
{
    public class AudioClipLibrary : MonoBehaviour
    {
        [SerializeField] private List<EntitySoundSet> _entitySounds;

        [SerializeField] private LevelObjectSoundSet _levelObjectSoundSet;

        [SerializeField] private TrackSet _levelTrackSet;

        private Dictionary<EntityKey, ISoundSet> _soundDict = new();

        private void Awake()
        {
            // Add the entities
            foreach (var soundSet in _entitySounds)
            {
                Register(soundSet.Entity, soundSet);
            }

            // Add level object sounds
            foreach (var entry in _levelObjectSoundSet.Entries.Select(e => e.Key).Distinct())
            {
                Register(entry, _levelObjectSoundSet);
            }

            // Try and squeeze songs and ambient tracks in here too
            foreach (var entry in _levelTrackSet.Entries.Select(e => e.Key).Distinct())
            {
                Register(entry, _levelTrackSet);
            }
        }
        private void Register(EntityKey key, ISoundSet set)
        {
            if (!_soundDict.TryAdd(key, set))
                Debug.LogError($"EntityKey {key} in sound set {set} is already registered to a sound set. Check for duplicate entries across your SoundSet assets.");
        }
        public AudioClipSettings LookUpClip(IAudioRequest request)
        {
            // try to get the clip
            if (_soundDict.TryGetValue(request.Entity, out ISoundSet soundSet))
            {
                return soundSet.GetClip(request);
            }
            Debug.LogError($"{request} was unhandled");
            return null;
        }
    }
}