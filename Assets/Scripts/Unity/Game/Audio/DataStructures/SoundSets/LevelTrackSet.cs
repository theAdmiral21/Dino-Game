using System.Collections.Generic;
using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(fileName = "LevelTrackSet", menuName = "Game/Audio/LevelTrackSet")]
    public class LevelTrackSet : ScriptableObject, ISoundSet<ISongAudioRequest>
    {
        [SerializeField] public List<Song> TrackList = new List<Song>();
        private List<string> _trackNames;
        public List<string> TrackNames
        {
            get
            {
                return _trackNames;
            }
            private set
            {
                for (int i = 0; i < TrackList.Count; i++)
                {
                    _trackNames[i] = TrackList[i].TrackName;
                }
            }
        }

        public AudioClipSettings GetClip(ISongAudioRequest request)
        {
            Debug.Log($"Requested: {request.SongKey}");
            switch (request.SongKey)
            {
                case SongSoundKey.MainMenu:
                    {
                        foreach (var song in TrackList)
                        {
                            Debug.Log($"Searching for song");
                            if (song.SongKey == request.SongKey)
                            {
                                return new AudioClipSettings(song.SongClip, Vector2.one * request.Volume, Vector2.one);
                            }
                        }
                        break;
                    }
                case SongSoundKey.Factory:
                    {
                        foreach (var song in TrackList)
                        {
                            Debug.Log($"Searching for song");
                            if (song.SongKey == request.SongKey)
                            {
                                return new AudioClipSettings(song.SongClip, Vector2.one * request.Volume, Vector2.one);
                            }
                        }
                        break;
                    }
            }
            Debug.LogError($"Unable to find song with SongKey: {request.SongKey}");
            return null;
        }

        public float GetSongDuration(int index)
        {
            return TrackList[index].SongClip.length;
        }
        public float GetSongDuration(string name)
        {
            int index = GetSongIndex(name);
            return TrackList[index].SongClip.length;
        }

        // Get the song from its index
        public Song GetSongFromIndex(int index)
        {
            return TrackList[index];
        }
        // Get the song from the string name
        public Song GetSongFromName(string name)
        {
            foreach (Song song in TrackList)
            {
                if (song.TrackName.Equals(name))
                {
                    return song;
                }
            }
            return null;
        }

        // Get the song from the string name
        public int GetSongIndex(string name)
        {
            for (int i = 0; i < TrackList.Count; i++)
            {
                if (TrackList[i].TrackName == name)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetSongIndex(Song song)
        {
            for (int i = 0; i < TrackList.Count; i++)
            {
                if (TrackList[i] == song)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}