using System;
using Game.Core.Effects;
using Primitives.Audio.SoundKeys;

namespace NPC.Core.Effects
{
    public struct DeathEffect : IEffectResult
    {
        public bool Approved => true;

        public Type EffectType => typeof(DeathEffect);

        public ActionSoundKey SoundKey => ActionSoundKey.Die;

    }
}