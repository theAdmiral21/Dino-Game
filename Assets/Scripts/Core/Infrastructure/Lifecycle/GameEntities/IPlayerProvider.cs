using System.Collections.Generic;
using Primitives.Players;

namespace Infrastructure.Core.Lifecycle
{
    public interface IPlayerProvider
    {
        public HashSet<IPlayerInfo> Players { get; }
    }
}