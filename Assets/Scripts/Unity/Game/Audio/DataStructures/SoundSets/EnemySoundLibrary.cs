using System.Collections.Generic;
using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Primitives.Audio.EntityKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Enemy Sound Library")]
    public class EnemySoundLibrary : ScriptableObject, ISoundSet<IEnemyAudioRequest>
    {
        [SerializeField] private List<EnemySoundSet> _entries = new();
        private Dictionary<EnemyEntityKey, EnemySoundSet> _audioDict = new();

        public AudioClipSettings GetClip(IEnemyAudioRequest request)
        {
            // Look up sound set by enemy entity key
            var enemyEntry = _audioDict[request.EntityKey];
            // Get audio clip from sound set with action key
            return enemyEntry.GetClip(request);
        }

        private void OnEnable()
        {
            BuildDictionary();
        }

        private void BuildDictionary()
        {
            _audioDict = new();
            foreach (var entry in _entries)
            {
                if (!_audioDict.ContainsKey(entry.EnemyType))
                {
                    _audioDict[entry.EnemyType] = entry;
                }
                else
                {
                    Debug.LogError($"Enemy sound set {entry.EnemyType} is already in the audio dictionary.");
                }
            }
        }
    }
}