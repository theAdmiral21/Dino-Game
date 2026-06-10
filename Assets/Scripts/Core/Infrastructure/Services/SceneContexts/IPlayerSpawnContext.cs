using System.Collections.Generic;
using Primitives.Players;

namespace Infrastructure.Core.Services
{
    public interface IPlayerSpawnContext
    {
        public void SetActivePlayers(HashSet<IPlayerInfo> players);

        public HashSet<IPlayerInfo> ConsumeActivePlayers();
    }
}