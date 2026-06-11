using System;
using Game.Core.Audio;
using Primitives.Audio;
using Primitives.Characters;

namespace Game.Application.Audio.DataStructures
{
    public class SpeakerSoundRequest : ISpeakerAudioRequest
    {
        public CharacterID EntityKey { get; private set; }

        public float Volume { get; set; }

        public AudioBehavior Behavior { get; private set; }

        public Type RequestType => typeof(EnemySoundRequest);

        public SpeakerSoundRequest(CharacterID entity, float volume = 1)
        {
            EntityKey = entity;
            Volume = volume;
            Behavior = AudioBehavior.SingleShot;
        }
    }
}