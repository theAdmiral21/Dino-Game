using PlayerController.Core.Info;
using PlayerController.Core.ManagerControls.Abstractions;

namespace PlayerController.Core.Events
{
    public record PlayerDiedEvent
    {
        public IPlayerView PlayerView;
        public IOverrideControls OverrideControls;
    }
}