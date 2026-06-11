using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{
    [CreateAssetMenu(fileName = "Song", menuName = "Audio/Song")]
    public class Song : ScriptableObject
    {
        [SerializeField] public string TrackName;
        public SongSoundKey SongKey;
        [SerializeField] public AudioClip SongClip;
        [SerializeField] public float PlaybackSpeed = 1;
        [SerializeField] public float LoopPoint;


        public float GetLoopPoint()
        {
            return LoopPoint;
        }
    }
}