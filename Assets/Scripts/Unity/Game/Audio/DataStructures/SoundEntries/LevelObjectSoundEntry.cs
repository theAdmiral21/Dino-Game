using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{
    /// <summary>
    /// Class for tying a level object's action to its associated audio.
    /// </summary>
    [System.Serializable]
    public class LevelObjectSoundEntry
    {
        public EntityKey Key;
        public ActionSoundKey ActionKey;
        public bool ModulatePitch;
        public bool ModulateVolume;
        public AudioClip Clip;
    }
}