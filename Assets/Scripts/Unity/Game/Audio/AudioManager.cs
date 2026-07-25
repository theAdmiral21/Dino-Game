using Game.Application.Audio;
using Game.Core.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Unity.Audio
{
    [RequireComponent(typeof(AudioListener))]
    [RequireComponent(typeof(AudioClipLibrary))]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        [SerializeField] private AudioMixer _mixer;

        [SerializeField] private AudioPlayer _audioPlayer;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"INSTANTIATED AudioManager {GetEntityId()}");
        }

        private void OnDestroy()
        {
            Debug.Log($"DESTROYED AudioManager {GetEntityId()}");
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public AudioSource PlaySound(IAudioRequest request)
        {

            return _audioPlayer.PlaySound(request);
        }

        public void StopSound(AudioSource source)
        {
            _audioPlayer.StopSound(source);
        }

        public AudioClip LookUpSound(IAudioRequest request)
        {
            return _audioPlayer.LookUpSound(request);
        }


    }
}