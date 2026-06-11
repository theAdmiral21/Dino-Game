// using Game.Core.Audio;
// using PlayerController.Core.Effects.DataStructures;
// using UnityEngine;

// namespace PlayerController.Unity.Audio
// {
//     /// <summary>
//     /// Helper class for connecting a given sound effect to an action.
//     /// </summary>
//     public class AudioEffectBridge : MonoBehaviour
//     {
//         [SerializeField] MonoBehaviour _audioPlayerMono;
//         private IPlayerAudioPlayer _audioPlayer;

//         private void Awake()
//         {
//             _audioPlayer = _audioPlayerMono as IPlayerAudioPlayer;
//             if (_audioPlayer == null)
//             {
//                 Debug.LogError($"{_audioPlayerMono.name} does not implement IAudioPlayer");
//             }
//         }
//         public void PlayBark()
//         {
//             _audioPlayer.PlayBarkSound();
//         }
//         public void PlayHowl()
//         {
//             _audioPlayer.PlayHowlSound();
//         }
//         public void PlayScentSound()
//         {
//             _audioPlayer.PlayScentSound();
//         }

//         public void PlayJump(JumpEffect jumpEffect)
//         {
//             _audioPlayer.PlayJumpSound(jumpEffect.Surface);
//         }

//         public void PlayDoubleJump(DoubleJumpEffect jumpEffect)
//         {
//             _audioPlayer.PlayDoubleJumpSound();
//         }

//         public void PlayWallJump(WallJumpEffect wallJumpEffect)
//         {
//             _audioPlayer.PlayJumpSound(wallJumpEffect.Surface);
//         }

//         public void PlayWalk(StepEffect stepEffect)
//         {
//             _audioPlayer.PlayWalkSound(stepEffect.Surface);
//         }
//         public void PlayRun(StepEffect stepEffect)
//         {
//             _audioPlayer.PlayRunSound(stepEffect.Surface);
//         }

//         public void PlayLanding(LandEffect landEffect)
//         {
//             _audioPlayer.PlayLandingSound(landEffect.Surface);
//         }

//         public void PlayWallSlide(WallSlideEffect wallSlideEffect)
//         {
//             _audioPlayer.PlayWallSlideSound(wallSlideEffect.Surface);
//         }

//         public void StopWallSlide()
//         {
//             _audioPlayer.StopWallSlideSound();
//         }
//         public void StopScentSound()
//         {
//             _audioPlayer.StopScentSound();
//         }

//         public void PlayZoomiesStartSound(ZoomiesEnterEffect zoomiesEffect) => _audioPlayer.PlayZoomiesStartSound();
//         public void PlayZoomiesTwinkleSound(ZoomiesTwinkleEffect zoomiesEffect) => _audioPlayer.PlayZoomiesTwinkleSound();
//         public void PlayZoomiesExitSound(ZoomiesExitEffect zoomiesEffect) => _audioPlayer.PlayZoomiesEndSound();

//     }
// }
