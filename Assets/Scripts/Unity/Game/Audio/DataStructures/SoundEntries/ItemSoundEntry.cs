using Primitives.Audio.EntityKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{
    [System.Serializable]
    public class ItemSoundEntry
    {
        public EntityKey Key;
        public AudioClip Clip;
    }
}