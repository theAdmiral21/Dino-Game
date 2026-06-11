using System;
using Game.Core.Audio;
using Primitives.Audio;

namespace Game.Application.Audio.DataStructures
{
    public class SlideSurfaceSoundRequest : ISlideSurfaceAudioRequest
    {
        public float Volume { get; set; }

        public Type RequestType => typeof(SlideSurfaceSoundRequest);

        public SurfaceType Surface { get; private set; }

        public AudioBehavior Behavior { get; private set; }

        public SlideSurfaceSoundRequest(SurfaceType surface, float volume = 1)
        {
            Surface = surface;
            Volume = volume;
            Behavior = AudioBehavior.Looping;
        }
    }
}