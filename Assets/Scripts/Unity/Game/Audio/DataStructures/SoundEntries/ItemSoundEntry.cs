using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{
    [System.Serializable]
    public class ItemSoundEntry
    {
        public ItemSoundKey Key;
        public AudioClip Clip;
    }
}