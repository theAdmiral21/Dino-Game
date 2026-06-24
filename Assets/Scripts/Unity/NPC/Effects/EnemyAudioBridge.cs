using UnityEngine;
using Game.Application.Audio.DataStructures;
using Infrastructure.Unity.Registries;
using Game.Core.Execution;
using Game.Core.Audio;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using NPC.Unity.Effects;
using Game.Core.Effects;
using NPC.Core.Effects;

namespace Game.Unity.Effects
{
    public class EnemyAudioBridge : BaseAudioBridge
    {
        [SerializeField] private EnemyEntityKey _entityKey;

        public override void HandleSound(IEffectResult audioEffect)
        {
            switch (audioEffect)
            {
                case AlertEffect alert:
                    {
                        PlayAlertSound();
                        break;
                    }
                case DeathEffect alert:
                    {
                        Debug.Log($"Playing death sound");
                        PlayDeathSound();
                        break;
                    }
                case AttackEffect alert:
                    {
                        Debug.Log($"Playing attack sound");
                        PlayAttackSound();
                        break;
                    }
            }
        }

        private void PlayAlertSound(float volume = 1f, bool loop = false)
        {
            _audioService.PlaySFX(new EnemySoundRequest(_entityKey, ActionSoundKey.Bark));
        }

        private void PlayAttackSound(float volume = 1f, bool loop = false)
        {
            _audioService.PlaySFX(new EnemySoundRequest(_entityKey, ActionSoundKey.Attack));
        }

        private void PlayDeathSound(float volume = 1f, bool loop = false) => _audioService.PlaySFX(new EnemySoundRequest(_entityKey, ActionSoundKey.Die));

        private void PlayWarCry(float volume = 1f, bool loop = false) => _audioService.PlaySFX(new EnemySoundRequest(_entityKey, ActionSoundKey.WarCry));
    }
}