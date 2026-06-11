using System;
using Game.Core.Audio;
using Primitives.Audio;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;

namespace Game.Application.Audio
{
    public class PlayerAudioRequest : IPlayerAudioRequest
    {
        public PlayerEntityKey PlayerEntity { get; private set; }
        public ActionSoundKey ActionKey { get; private set; }

        public float Volume { get; set; }

        public AudioBehavior Behavior { get; private set; }

        public Type RequestType => typeof(PlayerAudioRequest);

        public PlayerAudioRequest(PlayerEntityKey playerEntity, ActionSoundKey actionSoundKey, float volume = 1)
        {
            PlayerEntity = playerEntity;
            ActionKey = actionSoundKey;
            Volume = volume;
            Behavior = AudioBehavior.SingleShot;
        }
    }
}