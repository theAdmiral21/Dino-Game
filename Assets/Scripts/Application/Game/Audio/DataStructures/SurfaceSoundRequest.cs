using System;
using Game.Core.Audio;
using Primitives.Audio;
using Primitives.Audio.SoundKeys;

namespace Game.Application.Audio.DataStructures
{
    public class SurfaceSoundRequest : ISurfaceAudioRequest
    {
        public ActionSoundKey ActionKey { get; private set; }

        public float Volume { get; set; }

        public AudioBehavior Behavior { get; private set; }

        public Type RequestType => typeof(SurfaceSoundRequest);

        public SurfaceType Surface { get; private set; }

        public SurfaceSoundRequest(ActionSoundKey actionSoundKey, SurfaceType surface, float volume = 1)
        {
            ActionKey = actionSoundKey;
            Surface = surface;
            Volume = volume;
            Behavior = AudioBehavior.SingleShot;
        }
    }
}