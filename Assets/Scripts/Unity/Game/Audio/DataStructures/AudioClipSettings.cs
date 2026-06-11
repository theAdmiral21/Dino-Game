using Game.Unity.Audio.Abstractions;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{
    public class AudioClipSettings : IAudioClipSettings
    {
        public AudioClip Clip { get; private set; }
        public Vector2 VolumeRange { get; private set; }
        public Vector2 PitchRange { get; private set; }

        public AudioClipSettings(AudioClip clip, Vector2 volumeRange, Vector2 pitchRange)
        {
            Clip = clip;
            VolumeRange = volumeRange;
            PitchRange = pitchRange;
        }
    }
}