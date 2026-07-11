
using System.Collections.Generic;
using Game.Core.Audio;
using Game.Unity.Audio.DataStructures;


// using Primitives.Common.Audio;
using UnityEngine;

namespace Game.Unity.Audio.Abstractions
{
    public abstract class BaseSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourcePreFab;
        [SerializeField] private int _queueSize = 10;
        private Queue<AudioSource> _queuedSources = new();
        private List<AudioSource> _activeSources = new();

        private void Awake()
        {
            // Fill the queue
            for (int i = 0; i < _queueSize; i++)
            {
                var source = Instantiate(_audioSourcePreFab, transform);
                source.playOnAwake = false;
                _queuedSources.Enqueue(source);
            }
        }

        private void Update()
        {
            // Recycle finished sources
            for (int i = _activeSources.Count - 1; i >= 0; i--)
            {
                if (!_activeSources[i].isPlaying)
                {
                    _queuedSources.Enqueue(_activeSources[i]);
                    _activeSources[i].gameObject.SetActive(false);
                    _activeSources.RemoveAt(i);
                }
            }
        }

        protected AudioSource PlayOneShot(AudioClipSettings clipSettings, IAudioRequest request)
        {
            return PlaySFX(clipSettings, request.VolumeRange, false);
        }

        protected AudioSource PlayLooping(AudioClipSettings clipSettings, IAudioRequest request)
        {
            return PlaySFX(clipSettings, request.VolumeRange, true);
        }

        protected AudioSource PlayMusic(AudioClipSettings clipSettings, IAudioRequest request)
        {
            if (request.Behavior == Primitives.Audio.AudioBehavior.Music)
            {
                return PlaySFX(clipSettings, request.VolumeRange, true);
            }
            return PlaySFX(clipSettings, request.VolumeRange, false);
        }

        protected AudioSource PlayAmbient(AudioClipSettings clipSettings, IAudioRequest request)
        {
            return new AudioSource();
        }

        /// <summary>
        /// Method for playing a sound
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="loop"></param>
        /// <returns>AudioSource</returns>
        private AudioSource PlaySFX(AudioClipSettings clipSettings, Vector2 volumeRange, bool loop = false)
        {
            if (clipSettings == null || _queuedSources.Count == 0) return null;
            // Get the audio source from the queue
            var source = _queuedSources.Dequeue();
            // Set up the audio source
            source.clip = clipSettings.Clip;
            source.volume = RandomRange(clipSettings.VolumeRange);
            source.pitch = RandomRange(clipSettings.PitchRange);
            source.loop = loop;
            source.gameObject.SetActive(true);
            // Debug.Log($"Playing: {clip.name}");
            source.Play();

            // If the audio source isn't being looped, save it for later
            if (!loop)
            {
                _activeSources.Add(source);
            }
            return source;
        }

        /// <summary>
        /// Method for stopping a sound
        /// </summary>
        /// <param name="source"></param>
        protected void StopSFX(AudioSource source)
        {
            if (source == null) return;
            // Stop the clip and reset the audio source's settings
            ResetSoundSettings(source);

            // if the audio source isn't in the queue, add it
            if (!_queuedSources.Contains(source))
            {
                _queuedSources.Enqueue(source);
            }
            // remove the source from the active sources list
            _activeSources.Remove(source);
        }

        /// <summary>
        /// Stops all playing sounds.
        /// </summary>
        protected void StopAll()
        {
            // Find all active sources and stop them
            foreach (AudioSource source in _activeSources)
            {
                // Stop the clip and reset the audio source's settings
                ResetSoundSettings(source);
            }
        }

        private void ResetSoundSettings(AudioSource source)
        {
            // Stop the clip and reset the audio source's settings
            source.Stop();
            source.loop = false;
            source.clip = null;
            source.gameObject.SetActive(false);
            source.gameObject.transform.position = gameObject.transform.position;
        }

        private float RandomRange(Vector2 variationRange)
        {
            return Random.Range(variationRange.x, variationRange.y);
        }
    }
}