using UnityEngine;
using Game.Application.Audio.DataStructures;
using Infrastructure.Unity.Registries;
using Game.Core.Execution;
using Primitives.Audio;
using Game.Core.Audio;
using Primitives.Audio.SoundKeys;
using System;

namespace Game.Unity.Audio
{
    public class PlayerAudioBridge : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>, IPlayerAudioPlayer
    {
        private IAudioService _audioService;

        private AudioSource _wallSlideSource;
        private AudioSource _scentSource;
        private AudioSource _twinkleSource;

        public int Priority => 1;

        public void Initialize(IGameContext context)
        {
            _audioService = context.AudioService;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_audioService != null, $"Audio service is null");
        }

        public void PlayBark(float volume = 1f, bool loop = false) => _audioService.PlaySFX(new PlayerSoundRequest(ActionSoundKey.Bark));

        public void PlayHowl(float volume = 1f, bool loop = false) => _audioService.PlaySFX(new PlayerSoundRequest(ActionSoundKey.Howl));


        public void PlayWalk(SurfaceType surface, float volume = 1f, bool loop = false) => _audioService.PlaySFX(new SurfaceSoundRequest(ActionSoundKey.Walk, surface));

        public void PlayRun(SurfaceType surface, float volume = 1f, bool loop = false) => _audioService.PlaySFX(new SurfaceSoundRequest(ActionSoundKey.Run, surface));


        public void PlayJump(SurfaceType surface, float volume = 1f, bool loop = false) => _audioService.PlaySFX(new SurfaceSoundRequest(ActionSoundKey.Jump, surface));


        public void PlayLanding(SurfaceType surface, float volume = 1f, bool loop = false)
        {
            _audioService.PlaySFX(new SurfaceSoundRequest(ActionSoundKey.Land, surface));
        }

        public void PlayWallSlide(SurfaceType surface, float volume = 1, bool loop = false)
        {
            // Prevent playing multiple wall slide sounds at once
            if (_wallSlideSource == null)
            {
                // Debug.Log($"Playing wall slide effect");
                _wallSlideSource = _audioService.PlaySFX(new SlideSurfaceSoundRequest(surface));
            }
        }

        public void StopWallSlide()
        {
            // This is here to speed up execution because this is called every tick. Which might not be ideal.
            if (_wallSlideSource != null)
            {
                // Debug.Log($"Stopping wall slide effect");
                _audioService.StopSound(_wallSlideSource);
                _wallSlideSource = null;
            }
        }

        public void PlayScent(float volume = 1, bool loop = false)
        {
            throw new System.NotImplementedException();
        }

        public void StopScent()
        {
            if (_scentSource != null)
            {
                // Debug.Log($"Stopping wall slide effect");
                _audioService.StopSound(_scentSource);
                _scentSource = null;
            }
        }

        public void PlayDoubleJump(float volume = 1, bool loop = false) => _audioService.PlaySFX(new PlayerSoundRequest(ActionSoundKey.DoubleJump));

        public void PlayZoomiesStart(float volume = 1, bool loop = false) => _audioService.PlaySFX(new PlayerSoundRequest(ActionSoundKey.StartZoomies));

        public void PlayZoomiesEnd(float volume = 1, bool loop = false) => _audioService.PlaySFX(new PlayerSoundRequest(ActionSoundKey.EndZoomies));

        public void PlayZoomiesTwinkle(float volume = 1, bool loop = false)
        {
            // Prevent playing multiple wall slide sounds at once
            if (_twinkleSource == null)
            {
                // Debug.Log($"Playing wall slide effect");
                _twinkleSource = _audioService.PlaySFX(new PlayerSoundRequest(ActionSoundKey.ZoomiesTwinkle));
            }
        }

        public void StopZoomies()
        {
            if (_twinkleSource != null)
            {
                // Debug.Log($"Stopping wall slide effect");
                _audioService.StopSound(_twinkleSource);
                _twinkleSource = null;
            }
        }
    }
}