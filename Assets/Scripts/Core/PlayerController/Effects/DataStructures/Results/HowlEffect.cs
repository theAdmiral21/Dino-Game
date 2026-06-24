using System;
using Game.Core.Effects;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct HowlEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;


        public Type EffectType => typeof(HowlEffect);

        public PlayerSoundKey SoundKey => PlayerSoundKey.Howl;


        public HowlEffect(bool approved)
        {
            _approved = approved;
        }
    }
}