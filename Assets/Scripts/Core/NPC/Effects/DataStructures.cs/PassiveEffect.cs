using System;
using Game.Core.Effects;
using Primitives.Audio.Enums;
using Primitives.Audio.SoundKeys;

namespace NPC.Core.Effects
{
    public struct PassiveEffect : IEffectResult
    {
        public bool Approved => true;

        public Type EffectType => typeof(PassiveEffect);

        public ActionSoundKey SoundKey => ActionSoundKey.Bark;
    }
}