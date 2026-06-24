using System;
using Game.Core.Effects;
using Primitives.Audio;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct WallSlideEffect : IEffectResult
    {
        // NOTE Approved for this particular effect controls whether or not the sound is played. When the wall slide is denied, that is a signal to stop playing the wall slide sound.
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(WallSlideEffect);

        public PlayerSoundKey SoundKey => PlayerSoundKey.WallSlide;

        public readonly SurfaceType Surface;

        public WallSlideEffect(bool approved, SurfaceType surface)
        {
            _approved = approved;
            Surface = surface;
        }
    }
}