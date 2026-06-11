using Game.Core.Audio;
using UnityEngine;

namespace Game.Unity.Audio.Services
{
    public class AudioService : IAudioService
    {
        private AudioManager _audioManager;
        public AudioService(AudioManager audioManager)
        {
            if (audioManager == null)
            {
                Debug.LogError($"Audio manager has not be instantiated yet.");
            }
            _audioManager = audioManager;
        }
        public void PlayAmbient(IAudioRequest request)
        {
            throw new System.NotImplementedException();
        }

        public void PlayMusic(IAudioRequest request)
        {
            Debug.Log($"Requesting music");
            _audioManager.PlaySound(request);
        }

        public AudioSource PlaySFX(IAudioRequest request)
        {
            return _audioManager.PlaySound(request);
        }

        public void StopSound(AudioSource source)
        {
            _audioManager.StopSound(source);
        }
    }
}