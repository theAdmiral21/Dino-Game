using System;
using Game.Core.Effects;
using PlayerController.Core.Effects.Abstractions;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct RunEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(RunEffect);

        // Sound is handled by the step effect
        public PlayerSoundKey SoundKey => PlayerSoundKey.None;

        public readonly float InputValue;

        public RunEffect(bool approved, float inputValue)
        {
            _approved = approved;
            InputValue = inputValue;
        }
    }
}