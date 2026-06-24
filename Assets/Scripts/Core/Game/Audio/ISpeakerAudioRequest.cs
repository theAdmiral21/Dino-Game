using Primitives.Characters;

namespace Game.Core.Audio
{
    public interface ISpeakerAudioRequest : IAudioRequest
    {
        public CharacterID EntityKey { get; }

    }
}