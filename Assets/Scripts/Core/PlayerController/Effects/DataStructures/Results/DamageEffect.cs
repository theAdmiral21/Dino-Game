using System;
using Game.Core.Effects;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct DamageEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(DamageEffect);


        public float Dt { get; private set; }
        public float RemainingTime { get; private set; }

        public PlayerSoundKey SoundKey => PlayerSoundKey.None;

        public DamageEffect(bool approved, float dt, float remainingTime)
        {
            _approved = approved;
            Dt = dt;
            RemainingTime = remainingTime;
        }
    }
}