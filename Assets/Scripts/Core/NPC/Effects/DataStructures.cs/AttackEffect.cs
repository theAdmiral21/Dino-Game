using System;
using Game.Core.Effects;
using Primitives.Audio.SoundKeys;

namespace NPC.Core.Effects
{
    public struct AttackEffect : IEffectResult
    {
        public bool Approved => true;

        public Type EffectType => typeof(AttackEffect);

        public ActionSoundKey SoundKey => ActionSoundKey.Die;
    }
}