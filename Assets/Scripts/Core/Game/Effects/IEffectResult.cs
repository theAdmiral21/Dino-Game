using System;

namespace Game.Core.Effects
{
    public interface IEffectResult
    {
        public bool Approved { get; }
        public Type EffectType { get; }
        // public PlayerSoundKey SoundKey { get; }
    }
}