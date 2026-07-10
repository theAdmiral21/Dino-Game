using System;
using Game.Core.Effects;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct BiteEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type EffectType => typeof(BiteEffect);


        public BiteEffect(bool approved)
        {
            _approved = approved;
        }
    }
}