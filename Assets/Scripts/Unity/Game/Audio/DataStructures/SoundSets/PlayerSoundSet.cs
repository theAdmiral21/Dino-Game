using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Player Sound Set")]
    public class PlayerSoundSet : ScriptableObject, ISoundSet<IPlayerAudioRequest>
    {
        public AudioClip[] Barks;
        public AudioClip[] Howls;
        public AudioClip[] Attack;
        public AudioClip[] Hurt;
        public AudioClip[] Talk;
        public AudioClip[] Scent;
        public AudioClip[] DoubleJump;
        public AudioClip EnterZoomies;
        public AudioClip ZoomiesTwinkle;
        public AudioClip ExitZoomies;


        public Vector2 VolumeRange = new Vector2(0.95f, 1.05f);
        public Vector2 PitchRange = new Vector2(0.95f, 1.05f);

        public AudioClipSettings GetClip(IPlayerAudioRequest request)
        {
            // use request.ActionKey to map the sound, the sound manager has already mapped the entity for you
            AudioClip sound;
            switch (request.ActionKey)
            {
                case ActionSoundKey.Bark:
                    {
                        sound = GetRandomSound(Barks);
                        break;
                    }
                case ActionSoundKey.Howl:
                    {
                        sound = GetRandomSound(Howls);
                        break;
                    }
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
                case ActionSoundKey.Talk:
                    {
                        sound = GetRandomSound(Talk);
                        break;
                    }
                case ActionSoundKey.DoubleJump:
                    {
                        sound = GetRandomSound(DoubleJump);
                        break;
                    }
                case ActionSoundKey.StartZoomies:
                    {
                        sound = EnterZoomies;
                        break;
                    }
                case ActionSoundKey.ZoomiesTwinkle:
                    {
                        sound = ZoomiesTwinkle;
                        break;
                    }
                case ActionSoundKey.EndZoomies:
                    {
                        sound = ExitZoomies;
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

        public AudioClip GetRandomBark()
        {
            int randVal = Random.Range(0, Barks.Length);
            return Barks[randVal];
        }

        public AudioClip GetRandomHowl()
        {
            int randVal = Random.Range(0, Howls.Length);
            return Howls[randVal];
        }

        private AudioClip GetRandomSound(AudioClip[] actionSounds)
        {
            int randVal = Random.Range(0, actionSounds.Length);
            return actionSounds[randVal];
        }
    }
}