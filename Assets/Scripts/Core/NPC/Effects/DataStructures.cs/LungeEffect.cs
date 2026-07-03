using System;
using Game.Core.Effects;
using Primitives.Audio.SoundKeys;

namespace NPC.Core.Effects
{
    public struct LungeEffect : IEffectResult
    {
        public bool Approved => true;

        public Type EffectType => typeof(LungeEffect);

        public ActionSoundKey SoundKey => ActionSoundKey.Lunge;
    }
}