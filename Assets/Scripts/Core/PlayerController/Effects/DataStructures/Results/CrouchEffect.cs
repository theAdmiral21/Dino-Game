using System;
using Game.Core.Effects;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct CrouchEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(CrouchEffect);


        public PlayerSoundKey SoundKey => PlayerSoundKey.Crouch;
        public readonly bool CrouchValue;

        public CrouchEffect(bool approved, bool crouchValue)
        {
            _approved = approved;
            CrouchValue = crouchValue;
        }
    }
}