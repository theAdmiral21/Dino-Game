using Game.Core.Audio;
using Game.Unity.Audio.DataStructures;

namespace Game.Unity.Audio.Abstractions
{
    public interface ISoundSet<T> where T : IAudioRequest
    {
        public AudioClipSettings GetClip(T request);
    }
}