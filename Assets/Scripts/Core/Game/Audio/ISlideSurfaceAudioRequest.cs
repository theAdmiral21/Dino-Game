using Primitives.Audio;

namespace Game.Core.Audio
{
    public interface ISlideSurfaceAudioRequest : IAudioRequest
    {
        public SurfaceType Surface { get; }

    }
}