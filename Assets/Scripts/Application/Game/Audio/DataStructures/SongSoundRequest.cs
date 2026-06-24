using System;
using Game.Core.Audio;
using Primitives.Audio;

namespace Game.Application.Audio.DataStructures
{
    public class SongSoundRequest : ISongAudioRequest
    {
        public SongSoundKey SongKey { get; private set; }
        public bool Loop { get; private set; }

        public AudioBehavior Behavior { get; private set; }

        public Type RequestType => typeof(SongSoundRequest);

        public float Volume { get; set; }

        public SongSoundRequest(SongSoundKey songKey, AudioBehavior behavior = AudioBehavior.Music, float volume = 1)
        {
            SongKey = songKey;
            Behavior = behavior;
            Volume = volume;
        }
    }
}