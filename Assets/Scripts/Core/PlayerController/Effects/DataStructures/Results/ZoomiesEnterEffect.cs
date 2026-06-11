using System;
using Game.Core.Effects;
using PlayerController.Core.Effects.Abstractions;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct ZoomiesEnterEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(ZoomiesEnterEffect);


        public PlayerSoundKey SoundKey => PlayerSoundKey.StartedZoomies;

        public ZoomiesEnterEffect(bool approved)
        {
            _approved = approved;
        }
    }
}