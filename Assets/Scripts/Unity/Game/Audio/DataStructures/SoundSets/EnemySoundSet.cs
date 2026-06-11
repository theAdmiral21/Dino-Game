using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Enemy Sound Set")]
    public class EnemySoundSet : ScriptableObject, ISoundSet<IEnemyAudioRequest>
    {
        public EnemyEntityKey EnemyType => _enemyType;
        [SerializeField] private EnemyEntityKey _enemyType;
        public AudioClip[] Attack;
        public AudioClip[] Hurt;
        public AudioClip[] Die;
        public AudioClip[] WarCry;

        public Vector2 VolumeRange = new Vector2(0.95f, 1.05f);
        public Vector2 PitchRange = new Vector2(0.95f, 1.05f);

        public AudioClipSettings GetClip(IEnemyAudioRequest request)
        {
            // use request.ActionKey to map the sound, the sound manager has already mapped the entity for you
            AudioClip sound;
            switch (request.ActionKey)
            {
                case ActionSoundKey.Attack:
                    {
                        sound = GetRandomSound(Attack);
                        break;
                    }
                case ActionSoundKey.Hurt:
                    {
                        sound = GetRandomSound(Hurt);
                        break;
                    }
                case ActionSoundKey.Die:
                    {
                        sound = GetRandomSound(Die);
                        break;
                    }
                case ActionSoundKey.WarCry:
                    {
                        sound = GetRandomSound(WarCry);
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{name} could not map {request.ActionKey} to a sound.");
                        return null;
                    }
            }
            return new AudioClipSettings(sound, VolumeRange, PitchRange);
        }

        private AudioClip GetRandomSound(AudioClip[] actionSounds)
        {
            int randVal = Random.Range(0, actionSounds.Length);
            return actionSounds[randVal];
        }
    }
}