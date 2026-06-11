using Primitives.Characters;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{
    [System.Serializable]
    public class SpeakerSoundEntry
    {
        public CharacterID Key;
        public AudioClip Clip;
    }
}