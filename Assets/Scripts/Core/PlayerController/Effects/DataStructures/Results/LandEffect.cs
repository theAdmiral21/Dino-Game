using System;
using Game.Core.Effects;
using Primitives.Audio;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct LandEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(LandEffect);


        public PlayerSoundKey SoundKey => PlayerSoundKey.Land;
        public readonly SurfaceType Surface;

        public LandEffect(bool approved, SurfaceType surface)
        {
            _approved = approved;
            Surface = surface;
        }
    }
}