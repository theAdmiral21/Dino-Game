using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Menu Sound Set")]
    public class MenuSoundSet : ScriptableObject, ISoundSet<IMenuAudioRequest>
    {
        public AudioClip StartSound;
        public AudioClip SelectSound;
        public AudioClip SubmitSound;
        public AudioClip BackSound;
        public AudioClip CancelSound;

        public AudioClipSettings GetClip(IMenuAudioRequest request)
        {
            // use request.ActionKey to map the sound, the sound manager has already mapped the entity for you
            AudioClip sound;
            switch (request.ActionKey)
            {
                case MenuSoundKey.Start:
                    {
                        sound = StartSound;
                        break;
                    }
                case MenuSoundKey.Select:
                    {
                        sound = SelectSound;
                        break;
                    }
                case MenuSoundKey.Submit:
                    {
                        sound = SubmitSound;
                        break;
                    }
                case MenuSoundKey.Back:
                    {
                        sound = BackSound;
                        break;
                    }
                case MenuSoundKey.Cancel:
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