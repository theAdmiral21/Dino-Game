using PlayerController.Core.Info;

namespace PlayerController.Core.Events
{
    public record PlayerSpawnedEvent
    {
        public IPlayerView PlayerView;
    }
}