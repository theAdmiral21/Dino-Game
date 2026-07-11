using Game.Core.Audio;
using Game.Unity.Audio.DataStructures;

namespace Game.Unity.Audio.Abstractions
{
    public interface ISoundSet
    {
        public AudioClipSettings GetClip(IAudioRequest request);
    }
}