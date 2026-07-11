using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Menu Sound Set")]
    public class MenuSoundSet : ScriptableObject, ISoundSet
    {
        public AudioClip StartSound;
        public AudioClip SelectSound;
        public AudioClip SubmitSound;
        public AudioClip BackSound;
        public AudioClip CancelSound;

        public AudioClipSettings GetClip(IAudioRequest request)
        {
            // use request.ActionKey to map the sound, the sound manager has already mapped the entity for you
            AudioClip sound;
            switch (request.ActionKey)
            {
                case ActionSoundKey.Start:
                    {
                        sound = StartSound;
                        break;
                    }
                case ActionSoundKey.Select:
                    {
                        sound = SelectSound;
                        break;
                    }
                case ActionSoundKey.Submit:
                    {
                        sound = SubmitSound;
                        break;
                    }
                case ActionSoundKey.Back:
                    {
                        sound = BackSound;
                        break;
                    }
                case ActionSoundKey.Cancel:
                    {
                        sound = CancelSound;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{name} could not map {request.ActionKey} to a sound.");
                        return null;
                    }
            }
            return new AudioClipSettings(sound, Vector2.one, Vector2.one);
        }
    }
}