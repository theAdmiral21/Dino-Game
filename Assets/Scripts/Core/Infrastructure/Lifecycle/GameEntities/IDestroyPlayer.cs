using Primitives.Players;

namespace Infrastructure.Core.Lifecycle
{
    public interface IDestroyPlayer
    {
        public void DestroyPlayer(IPlayerInfo playerInfo);
    }
}