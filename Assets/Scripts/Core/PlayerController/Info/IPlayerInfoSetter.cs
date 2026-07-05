using PlayerController.Core.Info;
using Primitives.Players;

namespace Core.PlayerController.Info
{
    public interface IPlayerInfoSetter : IPlayerInfoProvider
    {
        public void SetPlayerInfo(IPlayerInfo playerInfo);
    }
}