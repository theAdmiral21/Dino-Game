using System.Collections.Generic;
using Core.Game.Audio;
using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(fileName = "LevelTrackSet", menuName = "Game/Audio/LevelTrackSet")]
    public class TrackSet : ScriptableObject, ISoundSet
    {
        public List<TrackSoundEntry> Entries => _entries;
        [SerializeField] private List<TrackSoundEntry> _entries = new();
        private Dictionary<TrackKey, TrackSoundEntry> _audioDict = new();

        // private List<string> _trackNames;
        // public List<string> TrackNames
        // {
        //     get
        //     {
        //         return _trackNames;
        //     }
        //     private set
        //     {
        //         for (int i = 0; i < TrackList.Count; i++)
        //         {
        //             _trackNames[i] = TrackList[i].TrackName;
        //         }
        //     }
        // }

        private void OnEnable()
        {
            BuildDictionary();
        }

        public AudioClipSettings GetClip(IAudioRequest request)
        {
            ITrackRequest trackRequest = request as ITrackRequest;
            if (trackRequest == null)
            {
                Debug.LogError($"Unable to convert {request} to ITrackRequest. What did you pass this function?");
                return null;
            }

            if (_audioDict.TryGetValue(trackRequest.Track, out TrackSoundEntry entry))
            {

                return new AudioClipSettings(entry.Clip, request.VolumeRange, request.PitchRange);
            }
            Debug.LogError($"Unable to find song with TrackKey: {trackRequest.Track}");
            return null;
        }

        // public float GetSongDuration(int index)
        // {
        //     return TrackList[index].SongClip.length;
        // }
        // public float GetSongDuration(string name)
        // {
        //     int index = GetSongIndex(name);
        //     return TrackList[index].SongClip.length;
        // }

        // Get the song from its index
        // public Song GetSongFromIndex(int index)
        // {
        //     return TrackList[index];
        // }
        // // Get the song from the string name
        // public Song GetSongFromName(string name)
        // {
        //     foreach (Song song in TrackList)
        //     {
        //         if (song.TrackName.Equals(name))
        //         {
        //             return song;
        //         }
        //     }
        //     return null;
        // }

        // // Get the song from the string name
        // public int GetSongIndex(string name)
        // {
        //     for (int i = 0; i < TrackList.Count; i++)
        //     {
        //         if (TrackList[i].TrackName == name)
        //         {
        //             return i;
        //         }
        //     }
        //     return -1;
        // }

        // public int GetSongIndex(Song song)
        // {
        //     for (int i = 0; i < TrackList.Count; i++)
        //     {
        //         if (TrackList[i] == song)
        //         {
        //             return i;
        //         }
        //     }
        //     return -1;
        // }
        private void BuildDictionary()
        {
            _audioDict = new();
            foreach (var entry in _entries)
            {
                if (!_audioDict.ContainsKey(entry.TrackKey))
                {
                    _audioDict[entry.TrackKey] = entry;
                }
                else
                {
                    Debug.LogError($"TackKey {entry.TrackKey} is already paired with audio clip {entry.Clip} in the track dictionary.");
                }
            }
        }
    }

    [System.Serializable]
    public class TrackSoundEntry
    {
        public EntityKey Key => EntityKey.Song;
        public TrackKey TrackKey;
        public AudioClip Clip;
    }
}