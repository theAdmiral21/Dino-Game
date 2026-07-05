using PlayerController.Core.Info;
using PlayerController.Core.ManagerControls.Abstractions;
using Primitives.Players;

namespace PlayerController.Core.Events
{
    public record PlayerDiedEvent
    {
        public IPlayerInfo PlayerInfo;
        public IPlayerView PlayerView;
        public IOverrideControls OverrideControls;
    }
}