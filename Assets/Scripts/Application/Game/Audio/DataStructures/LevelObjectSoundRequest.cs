using System;
using Game.Core.Audio;
using Primitives.Audio;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;

namespace Game.Application.Audio.DataStructures
{
    public class LevelObjectSoundRequest : ILevelObjectAudioRequest
    {
        public LevelObjectEntityKey EntityKey { get; private set; }
        public ActionSoundKey ActionKey { get; private set; }
        public AudioBehavior Behavior { get; private set; }
        public float Volume { get; set; }

        public Type RequestType => typeof(LevelObjectSoundRequest);


        public LevelObjectSoundRequest(LevelObjectEntityKey entity, ActionSoundKey actionSoundKey, AudioBehavior behaviorKey = AudioBehavior.SingleShot, float volume = 1)
        {
            EntityKey = entity;
            ActionKey = actionSoundKey;
            Behavior = behaviorKey;
            Volume = volume;

        }
    }
}