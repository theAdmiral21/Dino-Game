using Primitives.Audio.SoundKeys;

namespace Game.Core.Audio
{
    public interface IItemAudioRequest : IAudioRequest
    {
        public ItemSoundKey ActionKey { get; }

    }
}