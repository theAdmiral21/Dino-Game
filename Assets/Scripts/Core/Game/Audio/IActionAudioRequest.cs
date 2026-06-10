using Primitives.Audio.SoundKeys;

namespace Game.Core.Audio
{
    public interface IActionAudioRequest : IAudioRequest
    {
        public ActionSoundKey ActionKey { get; }

    }
}