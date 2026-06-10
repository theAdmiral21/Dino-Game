using Primitives.Audio.SoundKeys;

namespace Game.Core.Audio
{
    public interface IMenuAudioRequest : IAudioRequest
    {
        public MenuSoundKey ActionKey { get; }

    }
}