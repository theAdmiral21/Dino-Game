using System;
using Game.Core.Effects;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct ZoomiesEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(ZoomiesEffect);

        public float Duration { get; private set; }
        public PlayerSoundKey SoundKey => PlayerSoundKey.StartedZoomies;

        public ZoomiesEffect(bool approved, float duration)
        {
            _approved = approved;
            Duration = duration;
        }
    }
}