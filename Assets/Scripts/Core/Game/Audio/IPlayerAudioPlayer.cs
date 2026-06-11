using Primitives.Audio;

namespace Game.Core.Audio
{
    //NOTE Evaluate if you really need to have a bool for looping. I think AudioBehavior covers everything.
    public interface IPlayerAudioPlayer
    {
        public void PlayBark(float volume = 1f, bool loop = false);
        public void PlayHowl(float volume = 1f, bool loop = false);
        public void PlayCrouch(float volume = 1f, bool loop = false);
        public void PlayDoubleJump(float volume = 1f, bool loop = false);
        public void PlayWalk(SurfaceType surface, float volume = 1f, bool loop = false);
        public void PlayRun(SurfaceType surface, float volume = 1f, bool loop = false);
        public void PlayJump(SurfaceType surface, float volume = 1f, bool loop = false);
        public void PlayLanding(SurfaceType surface, float volume = 1f, bool loop = false);
        public void PlayWallSlide(SurfaceType surface, float volume = 1f, bool loop = false);
        public void PlayZoomiesStart(float volume = 1f, bool loop = false);
        public void PlayZoomiesEnd(float volume = 1f, bool loop = false);
        public void PlayZoomiesTwinkle(float volume = 1f, bool loop = false);

        public void StopWallSlide();
        public void StopScent();
        public void StopZoomies();

    }
}