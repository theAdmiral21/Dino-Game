using System;
using Game.Core.Effects;
using Primitives.Audio.SoundKeys;

namespace NPC.Core.Effects
{
    public struct HurtEffect : IEffectResult
    {
        public bool Approved => true;

        public Type EffectType => typeof(HurtEffect);

        public ActionSoundKey SoundKey => ActionSoundKey.Hurt;

    }
}