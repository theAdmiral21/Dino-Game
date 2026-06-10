using System;
using Primitives.Audio;

namespace Game.Core.Audio
{
    public interface IAudioRequest
    {
        public AudioBehavior Behavior { get; }
        public Type RequestType { get; }
        public float Volume { get; }
    }
}