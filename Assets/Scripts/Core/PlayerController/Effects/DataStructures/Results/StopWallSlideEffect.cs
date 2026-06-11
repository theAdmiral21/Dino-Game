using System;
using Game.Core.Effects;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct StopWallSlideEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(StopWallSlideEffect);

        public PlayerSoundKey SoundKey => PlayerSoundKey.WallSlide;

        public StopWallSlideEffect(bool approved)
        {
            _approved = approved;

        }
    }
}