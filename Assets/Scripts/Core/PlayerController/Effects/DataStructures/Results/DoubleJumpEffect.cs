using System;
using Game.Core.Effects;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct DoubleJumpEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(JumpEffect);

        public PlayerSoundKey SoundKey => PlayerSoundKey.DoubleJump;

        public DoubleJumpEffect(bool approved)
        {
            _approved = approved;
        }
    }
}