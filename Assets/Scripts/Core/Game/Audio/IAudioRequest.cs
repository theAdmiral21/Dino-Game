using Primitives.Audio;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Core.Audio
{
    public interface IAudioRequest
    {
        public EntityKey Entity { get; }
        public ActionSoundKey ActionKey { get; }
        public AudioBehavior Behavior { get; }
        public Vector2 VolumeRange { get; }
        public Vector2 PitchRange { get; }
        public SurfaceType Surface { get; }
    }
}