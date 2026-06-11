using Primitives.Players;

namespace PlayerController.Core.Info
{
    public interface IPlayerInfoProvider
    {
        public IPlayerInfo PlayerInfo { get; }
        public void SetPlayerInfo(IPlayerInfo playerInfo);
    }
}