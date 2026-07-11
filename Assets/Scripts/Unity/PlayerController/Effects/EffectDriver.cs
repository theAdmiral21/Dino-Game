using System.Collections.Generic;
using Game.Core.Audio;
using Game.Core.Effects;
using NPC.Core.Effects;
using NPC.Unity.Effects;
using PlayerController.Application.Abstractions;
using PlayerController.Core.Effects.DataStructures;
using PlayerController.Unity.Animations;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.Enums;
using Primitives.Audio.SoundKeys;
using Unity.Common.Unity;
using UnityEngine;

namespace PlayerController.Unity.Physics
{
    public class EffectDriver : MonoBehaviour, IEffectDriver
    {
        // [SerializeField] private AudioEffectBridge _audioBridge;
        [SerializeField] private AudioBridge _audioBridge;
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
                case DodgeEffect dodge:
                    {
                        Debug.Log("Playing dodge sound");
                        _audioBridge.PlaySound(EntityKey.Player, ActionSoundKey.Dodge);
                        break;
                    }
                case JumpEffect jump:
                    {
                        // Debug.Log("Playing jump");
                        _audioBridge.PlaySound(EntityKey.Player, ActionSoundKey.Jump, jump.Surface);
                        break;
                    }
                case StepEffect step:
                    {
                        if (step.Approved)
                        {
                            // Debug.Log($"Playing step audio");
                            if (step.SoundKey == PlayerSoundKey.Run)
                            {
                                _audioBridge.PlaySound(EntityKey.Player, ActionSoundKey.Run, step.Surface);
                            }
                            else
                            {
                                _audioBridge.PlaySound(EntityKey.Player, ActionSoundKey.Walk, step.Surface);
                            }
                        }
                        break;
                    }
                case LandEffect landing:
                    {
                        // Debug.Log("Playing landing");
                        _audioBridge.PlaySound(EntityKey.Player, ActionSoundKey.Walk, landing.Surface);
                        break;
                    }
                case CrouchEffect crouching:
                    {
                        // Debug.Log("Playing landing");
                        _audioBridge.PlaySound(EntityKey.Player, ActionSoundKey.Crouch);
                        break;
                    }
                case HurtEffect hurt:
                    {
                        // Debug.Log("Playing landing");
                        _audioBridge.PlaySound(EntityKey.Player, ActionSoundKey.Hurt);
                        break;
                    }
                case DeathEffect death:
                    {
                        // Debug.Log("Playing landing");
                        _audioBridge.PlaySound(EntityKey.Player, ActionSoundKey.Die);
                        break;
                    }
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