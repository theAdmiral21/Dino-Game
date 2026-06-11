using System;
using Game.Core.Effects;
using PlayerController.Core.Effects.Abstractions;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct ScentEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(ScentEffect);


        public PlayerSoundKey SoundKey => PlayerSoundKey.Scent;

        public ScentEffect(bool approved)
        {
            _approved = approved;
        }
    }
}