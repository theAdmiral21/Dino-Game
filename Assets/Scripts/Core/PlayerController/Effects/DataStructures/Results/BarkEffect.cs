using System;
using Game.Core.Effects;
using PlayerController.Core.Effects.Abstractions;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct DodgeEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(DodgeEffect);


        public PlayerSoundKey SoundKey => PlayerSoundKey.Bark;

        public DodgeEffect(bool approved)
        {
            _approved = approved;
        }
    }
}