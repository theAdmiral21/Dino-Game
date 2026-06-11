using Primitives.Audio;

namespace Game.Unity.Audio.Abstractions
{
    public interface IAudioSourceType
    {
        // Make an enum for Audio types
        public AudioBehavior Type { get; }
    }
}