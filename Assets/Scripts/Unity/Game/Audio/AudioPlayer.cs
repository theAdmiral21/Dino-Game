using Game.Core.Audio;
using Game.Unity.Audio;
using Game.Unity.Audio.Abstractions;
using Game.Unity.Audio.DataStructures;
using Primitives.Audio;
using UnityEngine;

namespace Game.Application.Audio
{
    [RequireComponent(typeof(AudioClipLibrary))]
    public class AudioPlayer : BaseSoundPlayer
    {
        // This class needs to be able to take an audio request and build/track the appropriate audio source for it which is then routed to the BaseSoundPlayer to actually be played.

        [SerializeField] private AudioClipLibrary _clipLibrary;

        public AudioClip LookUpSound(IAudioRequest request)
        {
            Debug.Log($"_clipLibrary is null: {_clipLibrary == null}");
            AudioClipSettings clipSettings = _clipLibrary.LookUpClip(request);
            AudioClip clip = clipSettings.Clip;
            return clip;
        }

        public AudioSource PlaySound(IAudioRequest request)
        {

            // if it's a single shot thing just look up the clip and let it ride
            if (request.Behavior == AudioBehavior.SingleShot)
            {
                AudioClipSettings clipSettings = _clipLibrary.LookUpClip(request);
                return PlayOneShot(clipSettings, request);
            }
            else if (request.Behavior == AudioBehavior.Looping)
            {
                AudioClipSettings clipSettings = _clipLibrary.LookUpClip(request);
                // Looping sounds need to be handled differently than one shot sounds. They exist until told to stop
                return PlayLooping(clipSettings, request);
            }
            else if (request.Behavior == AudioBehavior.Music)
            {
                AudioClipSettings clipSettings = _clipLibrary.LookUpClip(request);
                // Music loops indefinitely and can be ducked. Also looped music might not always loop from the beginning
                Debug.Log($"Starting track: {clipSettings.Clip}");
                return PlayMusic(clipSettings, request);
            }
            else if (request.Behavior == AudioBehavior.Ambient)
            {
                AudioClipSettings clipSettings = _clipLibrary.LookUpClip(request);
                // Ambient noise depends on the location and loops indefinitely. It can probably be ducked as well.
                return PlayAmbient(clipSettings, request);
            }
            else
            {
                Debug.LogError($"{request} did not specify a valid AudioBehavior, {request.Behavior}");
                return null;
            }
        }

        // How do I stop a specific sound? 
        public void StopSound(AudioSource source)
        {
            StopSFX(source);
        }
    }
}