using Primitives.Audio;
using Primitives.Audio.SoundKeys;

namespace Game.Core.Audio
{
    public interface ISurfaceAudioRequest : IAudioRequest
    {
        public ActionSoundKey ActionKey { get; }
        public SurfaceType Surface { get; }

    }
}