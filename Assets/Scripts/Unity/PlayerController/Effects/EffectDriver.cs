using System.Collections.Generic;
using Game.Core.Audio;
using Game.Core.Effects;
using PlayerController.Application.Abstractions;
using PlayerController.Core.Effects.DataStructures;
using PlayerController.Unity.Animations;
using PlayerController.Unity.Effects;
using Primitives.Audio.Enums;
using Unity.Common.Unity;
using UnityEngine;

namespace PlayerController.Unity.Physics
{
    public class EffectDriver : MonoBehaviour, IEffectDriver
    {
        // [SerializeField] private AudioEffectBridge _audioBridge;
        [SerializeField] private SerializedInterface<IPlayerAudioPlayer> _audioBridgeMono;
        private IPlayerAudioPlayer _audioBridge => _audioBridgeMono.Interface;
        [SerializeField] private AnimatorEffectBridge _animatorBridge;

        [SerializeField] private SerializedInterface<IEffectPlayer> _zoomiesPlayerMono;
        private IEffectPlayer _zoomiesPlayer => _zoomiesPlayerMono.Interface;
        private List<IEffectResult> _queuedEffects = new List<IEffectResult>();

        private bool _zoomingLastFrame;

        public void EnqueueEffectResults(List<IEffectResult> effectResults)
        {
            if (effectResults == null) return;
            _queuedEffects.AddRange(effectResults);
        }

        private void Update()
        {
            // Apply the effects
            var effectResults = _queuedEffects;

            // Do your stuff
            foreach (var effect in effectResults)
            {
                HandleAudio(effect);
                HandleVisual(effect);
            }


            // Clear the list
            _queuedEffects.Clear();
        }

        private void HandleAudio(IEffectResult audioEffect)
        {
            // if (audioEffect.Target != EffectTarget.Sound)
            // {
            //     Debug.LogWarning($"Passed effect with target: {audioEffect.Target} to audio handler.");
            //     return;
            // }

            switch (audioEffect)
            {
                case DodgeEffect bark:
                    {
                        // Debug.Log("Playing bark");
                        _audioBridge.PlayBark();
                        break;
                    }
                case HowlEffect howl:
                    {
                        // Debug.Log("Playing howl");
                        _audioBridge.PlayHowl();
                        break;
                    }
                case JumpEffect jump:
                    {
                        // Debug.Log("Playing jump");
                        _audioBridge.PlayJump(jump.Surface);
                        break;
                    }
                case DoubleJumpEffect doubleJump:
                    {
                        // Debug.Log("Playing double jump audio");
                        _audioBridge.PlayDoubleJump();
                        break;
                    }
                case WallJumpEffect wallJump:
                    {
                        _audioBridge.PlayJump(wallJump.Surface);
                        break;
                    }
                case WallSlideEffect wallSlide:
                    {
                        if (wallSlide.Approved)
                        {
                            _audioBridge.PlayWallSlide(wallSlide.Surface);
                        }
                        else
                        {
                            _audioBridge.StopWallSlide();
                        }
                        break;
                    }
                case StepEffect step:
                    {
                        if (step.Approved)
                        {
                            // Debug.Log($"Playing step audio");
                            if (step.SoundKey == PlayerSoundKey.Run)
                            {
                                _audioBridge.PlayRun(step.Surface);
                            }
                            else
                            {
                                _audioBridge.PlayWalk(step.Surface);
                            }
                        }
                        break;
                    }
                case LandEffect landing:
                    {
                        // Debug.Log("Playing landing");
                        _audioBridge.PlayLanding(landing.Surface);
                        break;
                    }
                case ScentEffect scent:
                    {
                        if (scent.Approved)
                        {
                            _audioBridge.PlayScent();
                        }
                        else
                        {
                            _audioBridge.StopScent();
                        }
                        break;
                    }
                case ZoomiesEffect zoomies:
                    {
                        _zoomiesPlayer.Play(zoomies);
                        break;
                    }
                    // case ZoomiesEnterEffect zoomies:
                    //     {
                    //         _audioBridge.PlayZoomiesStart();

                    //         break;
                    //     }
                    // case ZoomiesTwinkleEffect zoomies:
                    //     {
                    //         _audioBridge.PlayZoomiesTwinkle();
                    //         break;
                    //     }
                    // case ZoomiesExitEffect zoomies:
                    //     {
                    //         _audioBridge.PlayZoomiesEnd();
                    //         break;
                    //     }
            }
        }

        private void HandleVisual(IEffectResult visualEffect)
        {
            // if (visualEffect.Target != EffectTarget.Sound)
            // {
            //     Debug.LogWarning($"Passed effect with target: {visualEffect.Target} to visual handler.");
            //     return;
            // }
            // Debug.Log($"Got visual effect: {visualEffect}");
            _animatorBridge.ApplyEffect(visualEffect);

            // switch (visualEffect)
            // {
            //     case ZoomiesEnterEffect zoomies:
            //         {
            //             _lightController.EnterZoomiesMode();
            //             break;
            //         }
            //     case ZoomiesTwinkleEffect zoomies:
            //         {
            //             _lightController.UpdateZoomiesMode();
            //             break;
            //         }
            //     case ZoomiesExitEffect zoomies:
            //         {
            //             _lightController.ExitZoomiesMode();
            //             break;
            //         }
            // }
        }

    }
}