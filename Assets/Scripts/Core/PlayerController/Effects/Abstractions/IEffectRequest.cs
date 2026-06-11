using System;

namespace PlayerController.Core.Effects.Abstractions
{
    public interface IEffectRequest
    {
        public bool Requested { get; }
        public Type EffectType { get; }
    }
}