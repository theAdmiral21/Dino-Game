using Primitives.Audio.SoundKeys;

namespace Game.Core.Audio
{
    public interface IPlayerAudioRequest : IAudioRequest
    {
        public ActionSoundKey ActionKey { get; }

    }
}