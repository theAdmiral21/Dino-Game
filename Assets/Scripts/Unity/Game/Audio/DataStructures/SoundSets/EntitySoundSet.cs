using System.Collections.Generic;
using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Entity Sound Set")]
    public class EntitySoundSet : ScriptableObject, IEntitySoundSet
    {
        public EntityKey Entity => _entity;
        [Header("Asset Entity Type")]
        [SerializeField] private EntityKey _entity;

        [Header("Audio Clips")]
        [SerializeField] private List<EntitySoundEntry> _entries = new();
        // public AudioClip[] Attack;
        // public AudioClip[] Roar;
        // public AudioClip[] Hurt;
        // public AudioClip[] Die;

        private Dictionary<ActionSoundKey, AudioClip[]> _actionLookUp = new();

        private void OnEnable()
        {
            BuildDictionary();
        }

        public AudioClipSettings GetClip(IAudioRequest request)
        {
            AudioClip sound;

            if (_actionLookUp.TryGetValue(request.ActionKey, out AudioClip[] clips))
            {
                sound = GetRandomSound(clips);
                return new AudioClipSettings(sound, request.VolumeRange, request.PitchRange);
            }
            Debug.LogError($"{name} could not map {request.ActionKey} to a sound.");
            return null;
        }

        private AudioClip GetRandomSound(AudioClip[] clips)
        {
            int randVal = Random.Range(0, clips.Length);
            return clips[randVal];
        }

        private void BuildDictionary()
        {
            _actionLookUp = new();
            foreach (var entry in _entries)
            {
                if (!_actionLookUp.ContainsKey(entry.ActionKey))
                {
                    _actionLookUp[entry.ActionKey] = entry.Clips;
                }
                else
                {
                    Debug.LogError($"Action key: {entry.ActionKey} for entity {Entity} is already paired with audio clip array {entry.Clips} in the entity sound set dictionary.");
                }
            }
        }
    }

    [System.Serializable]
    public class EntitySoundEntry
    {
        public ActionSoundKey ActionKey;
        public AudioClip[] Clips;
    }
}
