using System;
using Game.Core.Effects;
using Primitives.Audio.SoundKeys;

namespace NPC.Core.Effects
{
    public struct ScanEffect : IEffectResult
    {
        public bool Approved => true;

        public Type EffectType => typeof(ScanEffect);

        public ActionSoundKey SoundKey => ActionSoundKey.Bark;
    }
}