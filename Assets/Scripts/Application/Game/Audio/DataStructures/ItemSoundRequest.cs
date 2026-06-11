using System;
using Game.Core.Audio;
using Primitives.Audio;
using Primitives.Audio.SoundKeys;

namespace Game.Application.Audio.DataStructures
{
    public class ItemSoundRequest : IItemAudioRequest
    {
        public ItemSoundKey ActionKey { get; private set; }

        public float Volume { get; set; }

        public AudioBehavior Behavior { get; private set; }

        public Type RequestType => typeof(ItemSoundRequest);

        public ItemSoundRequest(ItemSoundKey actionSoundKey, float volume = 1)
        {
            ActionKey = actionSoundKey;
            Volume = volume;
            Behavior = AudioBehavior.SingleShot;
        }
    }
}