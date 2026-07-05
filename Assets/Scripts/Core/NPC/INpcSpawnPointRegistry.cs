using System.Collections.Generic;

namespace Core.NPC
{
    public interface INpcSpawnPointRegistry
    {
        public IReadOnlyCollection<INpcSpawnPoint> SpawnPoints { get; }

    }
}