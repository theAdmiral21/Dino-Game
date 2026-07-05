// using Environment.Core.Level;
using PlayerController.Core.Effects.Abstractions;

namespace PlayerController.Core.Info
{
    public interface IPlayerView
    {
        // public IPlayerInfoProvider PlayerInfo { get; }
        public ITransitionView TransitionView { get; }
        // public ISpawnPoint CheckPoint { get; }

        public void SetTransitionView(ITransitionView view);
    }
}