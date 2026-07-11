using Game.Core.Audio;
using Primitives.Audio.SoundKeys;

namespace Core.Game.Audio
{
    public interface ITrackRequest : IAudioRequest
    {
        public TrackKey Track { get; }
    }
}