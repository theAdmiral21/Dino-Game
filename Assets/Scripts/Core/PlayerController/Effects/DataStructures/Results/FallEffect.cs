using System;
using Game.Core.Effects;
using PlayerController.Core.Effects.Abstractions;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct FallEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(FallEffect);

        public PlayerSoundKey SoundKey => PlayerSoundKey.None;

        public FallEffect(bool approved)
        {
            _approved = approved;
        }
    }
}