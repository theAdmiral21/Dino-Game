using System;
using Game.Core.Effects;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct ZoomiesTwinkleEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(ZoomiesTwinkleEffect);


        public PlayerSoundKey SoundKey => PlayerSoundKey.ZoomiesTwinkle;

        public ZoomiesTwinkleEffect(bool approved)
        {
            _approved = approved;
        }
    }
}