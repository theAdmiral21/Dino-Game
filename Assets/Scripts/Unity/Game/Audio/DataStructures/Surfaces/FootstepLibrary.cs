using Game.Core.Audio;
using Game.Unity.Audio.Abstractions;
using Primitives.Audio;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Footstep Library")]
    public class FootstepLibrary : ScriptableObject, ISoundSet<ISurfaceAudioRequest>
    {
        public SurfaceSoundSet DirtyGround;
        public SurfaceSoundSet Grass;
        public SurfaceSoundSet Gravel;
        public SurfaceSoundSet Leaves;
        public SurfaceSoundSet Metal;
        public SurfaceSoundSet Mud;
        public SurfaceSoundSet Rock;
        public SurfaceSoundSet Rug;
        public SurfaceSoundSet Sand;
        public SurfaceSoundSet Snow;
        public SurfaceSoundSet Stone;
        public SurfaceSoundSet Tile;
        public SurfaceSoundSet Water;
        public SurfaceSoundSet Wood;

        public Vector2 VolumeRange = new Vector2(0.95f, 1.05f);

        public Vector2 PitchRange = new Vector2(0.95f, 1.05f);

        public AudioClip GetWalkSoundEffect(SurfaceType surface)
        {
            return GetRandomWalk(surface);
        }

        public AudioClip GetRunSoundEffect(SurfaceType surface)
        {
            return GetRandomRun(surface);

        }
        public AudioClip GetJumpSoundEffect(SurfaceType surface)
        {

            return GetRandomJump(surface);
        }
        public AudioClip GetLandingSoundEffect(SurfaceType surface)
        {
            // Debug.Log("Get landing sound");
            return GetRandomLanding(surface);
        }

        public AudioClip GetRandomRun(SurfaceType surface)
        {
            SurfaceSoundSet set = GetSurfaceSet(surface);
            if (set == null) return null;
            return set.GetRandomRun();
        }
        public AudioClip GetRandomWalk(SurfaceType surface)
        {
            SurfaceSoundSet set = GetSurfaceSet(surface);
            if (set == null) return null;
            return set.GetRandomWalk();
        }
        public AudioClip GetRandomJump(SurfaceType surface)
        {
            SurfaceSoundSet set = GetSurfaceSet(surface);
            if (set == null) return null;
            return set.GetRandomJump();
        }
        public AudioClip GetRandomLanding(SurfaceType surface)
        {
            SurfaceSoundSet set = GetSurfaceSet(surface);
            if (set == null) return null;
            return set.GetRandomLanding();
        }

        private SurfaceSoundSet GetSurfaceSet(SurfaceType surface)
        {
            switch (surface)
            {
                case SurfaceType.DirtyGround:
                    {
                        return DirtyGround;
                    }
                case SurfaceType.Grass:
                    {
                        return Grass;
                    }
                case SurfaceType.Gravel:
                    {
                        return Gravel;
                    }
                case SurfaceType.Leaves:
                    {
                        return Leaves;
                    }
                case SurfaceType.Metal:
                    {
                        return Metal;
                    }
                case SurfaceType.Mud:
                    {
                        return Mud;
                    }
                case SurfaceType.Rock:
                    {
                        return Rock;
                    }
                case SurfaceType.Rug:
                    {
                        return Rug;
                    }
                case SurfaceType.Sand:
                    {
                        return Sand;
                    }
                case SurfaceType.Snow:
                    {
                        return Snow;
                    }
                case SurfaceType.Stone:
                    {
                        return Stone;
                    }
                case SurfaceType.Tile:
                    {
                        return Tile;
                    }
                case SurfaceType.Water:
                    {
                        return Water;
                    }
                case SurfaceType.Wood:
                    {
                        return Wood;
                    }
            }

            Debug.Log($"Could not find requested SoundSet: {surface}");
            return null;
        }

        public AudioClipSettings GetClip(ISurfaceAudioRequest request)
        {
            SurfaceType surface = request.Surface;
            ActionSoundKey action = request.ActionKey;
            AudioClip sound;
            switch (action)
            {
                case ActionSoundKey.Walk:
                    sound = GetRandomWalk(surface);
                    break;
                case ActionSoundKey.Run:
                    sound = GetRandomRun(surface);
                    break;
                case ActionSoundKey.Jump:
                    sound = GetRandomJump(surface);
                    break;
                case ActionSoundKey.Land:
                    sound = GetRandomLanding(surface);
                    break;
                default:
                    Debug.LogError($"Unable to map {action} to action.");
                    return null;
            }
            return new AudioClipSettings(sound, VolumeRange, PitchRange);
        }
    }
}