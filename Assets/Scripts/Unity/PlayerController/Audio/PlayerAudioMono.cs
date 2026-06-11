using System.Collections.Generic;
using UnityEngine;

namespace PlayerController.Unity.Audio
{
    /// <summary>
    /// Helper class for connecting a given sound effect to an action.
    /// </summary>
    public class PlayerAudioMono : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _jumpSounds = new List<AudioClip>();
        [SerializeField] private List<AudioClip> _sprintSounds = new List<AudioClip>();
        [SerializeField] private List<AudioClip> _landSounds = new List<AudioClip>();

        private void Awake()
        {

        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }

        private void PlayRandomSound(List<AudioClip> audioClips)
        {
            int randVal = Random.Range(0, audioClips.Count);
            // SoundFXManager.Instance.PlaySFX(audioClips[randVal], transform);
        }

        private void PlayJumpSound()
        {
            PlayRandomSound(_jumpSounds);
        }
        private void PlayLandSound()
        {
            PlayRandomSound(_landSounds);
        }
        private void PlaySprintSound()
        {
            PlayRandomSound(_sprintSounds);
        }



    }
}