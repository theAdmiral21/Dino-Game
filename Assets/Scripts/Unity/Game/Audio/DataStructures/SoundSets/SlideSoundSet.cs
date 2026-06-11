using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Primitives.Audio;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Slide surface Sound Set")]
    public class SlideSoundSet : ScriptableObject, ISoundSet<ISlideSurfaceAudioRequest>
    {
        public SurfaceType Surface;
        public AudioClip[] WallSlide;
        public Vector2 VolumeRange = new Vector2(0.95f, 1.05f);
        public Vector2 PitchRange = new Vector2(0.95f, 1.05f);

        public AudioClipSettings GetClip(ISlideSurfaceAudioRequest request)
        {
            // This is until I get other sounds I guess
            return new AudioClipSettings(WallSlide[0], VolumeRange, PitchRange);
        }


    }
}