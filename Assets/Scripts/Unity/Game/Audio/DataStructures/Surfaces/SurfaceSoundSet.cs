using Primitives.Audio;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Surface Sound Set")]
    public class SurfaceSoundSet : ScriptableObject
    {
        public SurfaceType Surface;
        public AudioClip[] Run;
        public AudioClip[] Walk;
        public AudioClip[] Jumps;
        public AudioClip[] Landings;
        public Vector2 VolumeVariation = new Vector2(1f, 1.15f);
        public Vector2 PitchVariation = new Vector2(0.95f, 1.05f);

        public AudioClip GetRandomRun()
        {
            int randVal = Random.Range(0, Run.Length);
            return Run[randVal];
        }

        public AudioClip GetRandomWalk()
        {
            int randVal = Random.Range(0, Walk.Length);
            return Walk[randVal];
        }

        public AudioClip GetRandomJump()
        {
            int randVal = Random.Range(0, Jumps.Length);
            return Jumps[randVal];
        }
        public AudioClip GetRandomLanding()
        {
            int randVal = Random.Range(0, Landings.Length);
            return Landings[randVal];
        }
    }
}