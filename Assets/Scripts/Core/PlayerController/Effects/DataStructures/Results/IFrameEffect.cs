using System;
using Game.Core.Effects;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct IFrameEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(IFrameEffect);

        public PlayerSoundKey SoundKey => PlayerSoundKey.None;

        public IFrameEffect(bool approved)
        {
            _approved = approved;
        }
    }
}