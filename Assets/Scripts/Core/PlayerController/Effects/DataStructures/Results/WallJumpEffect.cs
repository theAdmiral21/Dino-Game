using System;
using Game.Core.Effects;
using Primitives.Audio;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct WallJumpEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(WallJumpEffect);

        public PlayerSoundKey SoundKey => PlayerSoundKey.Jump;

        public readonly SurfaceType Surface;

        public WallJumpEffect(bool approved, SurfaceType surface)
        {
            _approved = approved;
            Surface = surface;
        }
    }
}