using UnityEngine;

namespace Game.Unity.Audio.Abstractions
{
    public interface IAudioClipSettings
    {
        public AudioClip Clip { get; }
        public Vector2 VolumeRange { get; }
        public Vector2 PitchRange { get; }
    }
}