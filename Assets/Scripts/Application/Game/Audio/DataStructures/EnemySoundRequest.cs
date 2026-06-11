using System;
using Game.Core.Audio;
using Primitives.Audio;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;

namespace Game.Application.Audio.DataStructures
{
    public class EnemySoundRequest : IEnemyAudioRequest
    {
        public EnemyEntityKey EntityKey { get; private set; }
        public ActionSoundKey ActionKey { get; private set; }

        public float Volume { get; set; }

        public AudioBehavior Behavior { get; private set; }

        public Type RequestType => typeof(EnemySoundRequest);

        public EnemySoundRequest(EnemyEntityKey entity, ActionSoundKey actionSoundKey, float volume = 1)
        {
            EntityKey = entity;
            ActionKey = actionSoundKey;
            Volume = volume;
            Behavior = AudioBehavior.SingleShot;
        }
    }
}