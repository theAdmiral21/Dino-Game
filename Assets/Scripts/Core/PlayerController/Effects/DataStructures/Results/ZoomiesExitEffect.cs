using System;
using Game.Core.Effects;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct ZoomiesExitEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(ZoomiesExitEffect);


        public PlayerSoundKey SoundKey => PlayerSoundKey.ExitedZoomies;

        public ZoomiesExitEffect(bool approved)
        {
            _approved = approved;
        }
    }
}