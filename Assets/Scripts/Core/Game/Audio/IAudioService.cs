
using UnityEngine;

namespace Game.Core.Audio
{
    public interface IAudioService
    {
        public AudioClip LookUpClip(IAudioRequest request);
        public AudioSource PlaySFX(IAudioRequest request);

        public void PlayMusic(IAudioRequest request);

        public void PlayAmbient(IAudioRequest request);

        public void StopSound(AudioSource source);
    }

}