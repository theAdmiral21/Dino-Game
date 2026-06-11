using System;
using Game.Core.Effects;
using PlayerController.Core.Effects.Abstractions;
using Primitives.Audio;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct JumpEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(JumpEffect);

        public PlayerSoundKey SoundKey => PlayerSoundKey.Jump;

        public readonly SurfaceType Surface;

        public JumpEffect(bool approved, SurfaceType surface)
        {
            _approved = approved;
            Surface = surface;
        }
    }
}