using System;
using Game.Core.Effects;
using Primitives.Audio;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct StepEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(StepEffect);

        public EffectTarget Target => EffectTarget.Sound;
        public PlayerSoundKey SoundKey => _key;
        private PlayerSoundKey _key;
        public readonly SurfaceType Surface;
        public readonly float Speed01;

        public StepEffect(bool approved, PlayerSoundKey key, SurfaceType surface, float speed01)
        {
            _approved = approved;
            _key = key;
            Surface = surface;
            Speed01 = speed01;
        }
    }
}