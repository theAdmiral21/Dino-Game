using Primitives.Audio;

namespace Game.Core.Audio
{
    //NOTE Evaluate if you really need to have a bool for looping. I think AudioBehavior covers everything.
    public interface IPlayerAudioPlayer
    {
        public void PlayDeath(float volume = 1f, bool loop = false);
        public void PlayHurt(float volume = 1f, bool loop = false);
        public void PlayDodge(float volume = 1f, bool loop = false);
        public void PlayCrouch(float volume = 1f, bool loop = false);
        public void PlayWalk(SurfaceType surface, float volume = 1f, bool loop = false);
        public void PlayRun(SurfaceType surface, float volume = 1f, bool loop = false);
        public void PlayJump(SurfaceType surface, float volume = 1f, bool loop = false);
        public void PlayLanding(SurfaceType surface, float volume = 1f, bool loop = false);


    }
}